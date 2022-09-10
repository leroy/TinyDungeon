using System.Collections.Generic;
using Godot;
using TinyDungeon.Util;

namespace TinyDungeon.World
{
    public class TileInfoData
    {
        private IdGenerator _idGenerator = new IdGenerator();
        
        private Dictionary<Vector2, TileInfo> _tiles = new Dictionary<Vector2, TileInfo>();

        private Dictionary<int, TileInfo> _tilesById = new Dictionary<int, TileInfo>();

        public TileInfo AddTile(Tile tile)
        {
            if (_tiles.ContainsKey(tile.Position))
            {
                return GetTile(tile);
            }
            
            int id = _idGenerator.Id();
            
            _tiles[tile.Position] = new TileInfo(id, tile);
            _tilesById[id] = _tiles[tile.Position];

            return _tiles[tile.Position];
        }

        public TileInfo GetTile(Tile tile)
        {
            if (!_tiles.ContainsKey(tile.Position))
            {
                return AddTile(tile);
            }
            
            return _tiles[tile.Position];
        }

        public TileInfo GetTileById(int id)
        {
            return _tilesById[id];
        }
    }
}