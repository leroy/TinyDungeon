using Godot;
using TinyDungeon.World;
using Random = TinyDungeon.Util.Random;

namespace TinyDungeon.Dungeon
{
    public class Room : ITileable, ITiled
    {
        private TileGrid _tiles;

        public TileGrid Tiles => GetGrid();

        public Vector2 Size => Tiles.Data.Size + _margin;
        
        public int Width => (int) _size.x;
        
        public int Height => (int) _size.y;

        private Vector2 _size;
        
        private Vector2 _margin = Vector2.Zero;

        private Vector2 NorthWestCorner => new Vector2(0, 0);
        private Vector2 NorthEastCorner => new Vector2(Width, 0);
        private Vector2 SouthEastCorner => new Vector2(Width, Height);
        private Vector2 SouthWestCorner => new Vector2(0, Height);

        private Vector2[] Corners => new Vector2[]
        {
            NorthWestCorner,
            NorthEastCorner,
            SouthEastCorner,
            SouthWestCorner
        };

        public Room(Vector2 size, Vector2? margin = null)
        {
            _size = size;
            _margin = margin ?? Vector2.Zero;

            _tiles = new TileGrid();
        }

        public Vector2 GetGridSize()
        {
            return _size;
        }

        public TileGrid GetGrid()
        {
            var grid = new TileGrid();

            grid.Data.Fill(GetGridSize(), TileType.FLOOR);

            grid.Data.Strip(new Vector2(1, 0), Vector2.Right, Width - 1, TileType.WALL_LEDGE);
            grid.Data.Strip(new Vector2(1, 1), Vector2.Right, Width - 1, TileType.WALL);
            
            grid.Data.Strip(new Vector2(1, 2), Vector2.Right, Width - 1, TileType.FLOOR_EDGE_NORTH);
            
            grid.Data.SetTile(new Vector2(1, 2), TileType.FLOOR_EDGE_NORTH_WEST);
            
            grid.Data.SetTile(NorthWestCorner, TileType.INNER_WALL_NORTH_WEST);
            grid.Data.SetTile(NorthEastCorner, TileType.INNER_WALL_NORTH_EAST);
            grid.Data.SetTile(SouthEastCorner, TileType.INNER_WALL_SOUTH_EAST);
            grid.Data.SetTile(SouthWestCorner, TileType.INNER_WALL_SOUTH_WEST);
            
            grid.Data.Strip(new Vector2(0, 1), Vector2.Down, Height - 1, TileType.WALL_EAST);
            grid.Data.Strip(new Vector2(Width, 1), Vector2.Down, Height - 1, TileType.WALL_WEST);
            grid.Data.Strip(new Vector2(1, 3), Vector2.Down, Height - 2, TileType.FLOOR_EDGE_EAST);
            
            grid.Data.Strip(new Vector2(1, Height), Vector2.Right, Width - 1, TileType.INNER_WALL_SOUTH);

            return grid;
        }

        public Vector2 GetDoorPosition()
        {
            Vector2[] doors = new[]
            {
                new Vector2(Mathf.Floor(Width / 2), -_margin.y),
                new Vector2(Mathf.Floor(Width / 2), Height + _margin.y),
                new Vector2(-_margin.x, Mathf.Floor(Height / 2)),
                new Vector2(Width + _margin.x, Mathf.Floor(Height / 2)),
            };

            return doors[Random.RandomIndex(doors)];
        }

        public Rect2 GetRect2(Vector2 position)
        {
            return new Rect2(position, GetGridSize() + _margin);
        }
    }
}