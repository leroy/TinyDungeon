using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace TinyDungeon.World
{
    public class TileGridData : IEnumerable<Tile>
    {
        private Vector2 _origin;

        private Vector2 _size;

        public Vector2 Size => _size;
        
        private Dictionary<Vector2, Tile> _tiles = new Dictionary<Vector2, Tile>();


        public TileGridData SetTile(Vector2 position, TileType type)
        {
            SetTile(new Tile(position, type));
            
            return this;
        }

        public TileGridData SetTile(Tile tile)
        {
            _tiles[tile.Position] = tile;
            
            UpdateBoundingBox(tile.Position);

            return this;
        }

        public Tile GetTile(Vector2 position)
        {
            if (!_tiles.ContainsKey(position)) return new Tile(position, TileType.EMPTY);
            
            return _tiles[position];
        }
        
        public IEnumerator<Tile> GetEnumerator()
        {
            return _tiles.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Tile this[Vector2 position]
        {
            get { return _tiles[position]; }
            set { _tiles[position] = value; }
        }

        public TileGridData Fill(Vector2 size, TileType type = TileType.EMPTY)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    SetTile(new Vector2(x, y), type);
                }
            }

            return this;
        }

        public TileGridData Strip(Vector2 position, Vector2 direction, int length, TileType type)
        {
            for (int i = 0; i < length; i++)
            {
                SetTile(position + direction * i, type);
            }

            return this;
        }

        public TileGridData Strip(Vector2 position, Vector2 direction, int length, TileType[] types)
        {
            foreach (var type in types.Select((type, index) => new { index, type }))
            {
                Strip(position + direction.Perpendicular() * type.index, direction, length, type.type);
            }

            return this;
        }

        public TileGridData Resize(Vector2 size)
        {
            var factor = _size / size;

            var grid = new TileGridData();

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    int tileX = (int) Math.Floor(factor.x * x);
                    int tileY = (int) Math.Floor(factor.y * y);

                    grid.SetTile(new Vector2(x, y), GetTile(new Vector2(tileX, tileY)).Type);
                }
            }

            return grid;
        }

        public TileGridData Resize(double factor)
        {
            return Resize(_size * new Vector2((float) factor, (float) factor));
        }

        private void UpdateBoundingBox(Vector2 position)
        {
            if (position < _origin)
            {
                _origin = position;
            }

            if (position > _origin + _size)
            {
                _size = position - _origin;
            }
        }
    }
}