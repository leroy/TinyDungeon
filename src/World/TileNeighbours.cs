using System.Collections;
using System.Collections.Generic;
using Godot;

namespace TinyDungeon.World
{
    public class TileNeighbours : IEnumerable<Tile>
    {
        private TileGrid _tilegrid;

        private Tile _tile;

        public Tile North => _tilegrid.Data.GetTile(_tile.Position + Vector2.Up);
        public Tile NorthWest => _tilegrid.Data.GetTile(_tile.Position + (Vector2.Up + Vector2.Left));
        public Tile NorthEast => _tilegrid.Data.GetTile(_tile.Position + (Vector2.Up + Vector2.Right));
        
        public Tile West => _tilegrid.Data.GetTile(_tile.Position + Vector2.Left);
        
        public Tile South => _tilegrid.Data.GetTile(_tile.Position + Vector2.Down);
        public Tile SouthWest => _tilegrid.Data.GetTile(_tile.Position + (Vector2.Down + Vector2.Left));
        public Tile SouthEast => _tilegrid.Data.GetTile(_tile.Position + (Vector2.Down + Vector2.Right));
        
        public Tile East => _tilegrid.Data.GetTile(_tile.Position - Vector2.Right);

        public TileNeighbours(TileGrid grid, Tile tile)
        {
            _tile = tile;
            _tilegrid = grid;
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            return new List<Tile>()
            {
                North,
                NorthEast,
                NorthWest,
                West,
                South,
                SouthWest,
                SouthEast,
                East
            }.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}