using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Godot;
using TinyDungeon.Algorithm;
using TinyDungeon.World;
using Random = TinyDungeon.Util.Random;
using Room = TinyDungeon.Dungeon.Room;

namespace TinyDungeon.WorldGenerator
{
    public class DungeonGenerator : IWorldGenerator
    {
        public class DungeonGeneratorSettings
        {
            public Vector2 Size = new Vector2(50, 50);

            public Vector2 MinRoomSize = new Vector2(5, 5);
            public Vector2 MaxRoomSize = new Vector2(10, 10);

            public int MinRooms = 5;

            public int MaxRooms = 10;
        }

        private DungeonGeneratorSettings _settings;

        private RandomNumberGenerator _randomNumberGenerator;

        private TileGrid _tileGrid;
        
        public DungeonGenerator(DungeonGeneratorSettings settings)
        {
            _settings = settings;
            _randomNumberGenerator = new RandomNumberGenerator();
            
            Reset();
        }

        public void Reset()
        {
            _tileGrid = new TileGrid();
        }

        public TileGrid GetResults()
        {
            return _tileGrid;
        }

        public void Generate()
        {
            _tileGrid.Data.Fill(_settings.Size);
            
            TiledStructure<Room>[] placedRooms = PlaceRooms();
            
            Pathfinder pathfinder = new Pathfinder(_tileGrid);
            pathfinder.AddFilter(PathableTileFilter(placedRooms.Select(s => s.Bounds).ToList()));
            pathfinder.Build();

            foreach (TiledStructure<Room> structureA in placedRooms)
            {
                Vector2 doorA = structureA.ToGlobalPosition(structureA.Structure.GetDoorPosition());
                
                foreach (TiledStructure<Room> structureB in placedRooms.Where(s => s.Structure != structureA.Structure).OrderBy(s => structureA.Center.DistanceTo(s.Center)).Take(_randomNumberGenerator.RandiRange(1, 2)))
                {
                    Vector2 doorB = structureB.ToGlobalPosition(structureB.Structure.GetDoorPosition());
            
                    Tile[] path = pathfinder.Find(_tileGrid.Data.GetTile(doorA), _tileGrid.Data.GetTile(doorB));
            
                    foreach (Tile tile in path)
                    {
                        _tileGrid.SetTile(tile.Position, TileType.FLOOR);
                    }
                    
                }
            }

        }

        private Func<Tile, bool> PathableTileFilter(List<Rect2> bounds)
        {
            return (Tile tile) => !bounds.Any(bound => bound.HasPoint(tile.Position));
        }

        private TiledStructure<Room>[] PlaceRooms()
        {
            Room[] rooms = MakeRooms();
            Rect2[] bounds = new Rect2[rooms.Length];


            TiledStructure<Room>[] structures = new TiledStructure<Room>[rooms.Length];

            foreach (var entry in rooms.Select((room, index) => new { room, index }))
            {
                Vector2 randomPosition;
                
                do
                {
                    randomPosition = Random.RandomVector2I(_settings.Size);
                } while (bounds.Any(rect => rect.Intersects(entry.room.GetRect2(randomPosition), true)) || !GetBounds().Encloses(entry.room.GetRect2(randomPosition)));


                structures[entry.index] = new TiledStructure<Room>(randomPosition, entry.room);
                structures[entry.index].MergeInto(_tileGrid);

                bounds[entry.index] = entry.room.GetRect2(randomPosition);
            }

            return structures;
        }

        private List<Tile> PathableTiles()
        {
            return _tileGrid.Data.Where(tile => tile.Type == TileType.EMPTY).ToList();
        }

        private Room[] MakeRooms()
        {
            int count = _randomNumberGenerator.RandiRange(_settings.MinRooms, _settings.MaxRooms);

            Room[] rooms = new Room[count];
            
            for (int i = 0; i < count; i++)
            {
                rooms[i] = GetRandomRoom();
            }

            return rooms;
        }

        private Room GetRandomRoom()
        {
            int width = _randomNumberGenerator.RandiRange((int) _settings.MinRoomSize.x, (int) _settings.MaxRoomSize.x);
            int height = _randomNumberGenerator.RandiRange((int) _settings.MinRoomSize.y, (int) _settings.MaxRoomSize.y);

            Vector2 size = new Vector2(width, height);
            
            return new Room(size, new Vector2(2, 2));
        }

        private Rect2 GetBounds()
        {
            return new Rect2(Vector2.Zero, _settings.Size);
        }
    }
}