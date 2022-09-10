using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

namespace TinyDungeon.World
{
    public class TileGrid : IEnumerable<Tile>
    {
        private TileGridData _data = new TileGridData();

        private TileInfoData _tileInfo = new TileInfoData();

        public TileGridData Data
        {
            get { return _data; }
        }

        public TileInfoData TileInfo
        {
            get { return _tileInfo; }
        }

        public void SetTile(Vector2 position, TileType type)
        {
            _data.SetTile(position, type);
            _tileInfo.AddTile(_data.GetTile(position));
        }

        public void Merge(TileGrid other, Vector2? offset = null)
        {
            if (offset == null)
            {
                offset = Vector2.Zero;
            }

            foreach (Tile tile in other.Data)
            {
                SetTile(tile.Position + (Vector2) offset, tile.Type);
            }
        }
        
        public IEnumerator<Tile> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}