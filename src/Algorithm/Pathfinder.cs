using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using TinyDungeon.World;
using TileSet = Godot.TileSet;

namespace TinyDungeon.Algorithm
{
    public class Pathfinder
    {
        private AStar2D _aStar = new AStar2D();

        private TileGrid _tileGrid;
        
        private List<Func<Tile, bool>> _filters = new List<Func<Tile, bool>>();
        
        public Pathfinder(TileGrid grid)
        {
            _tileGrid = grid;
        }

        public void AddFilter(Func<Tile, bool> constraint)
        {
            _filters.Add(constraint);
        }

        private Func<Tile, bool> FilterTiles()
        {
            return tile => _filters.All(filter => filter(tile));
        }

        public Pathfinder Build()
        {
            List<Tile> tiles = _tileGrid.Data.Where(FilterTiles()).ToList();
            
            _aStar.Clear();

            foreach (Tile tile in tiles)
            {
                _aStar.AddPoint(_tileGrid.TileInfo.GetTile(tile).Id, tile.Position);
            }

            foreach (Tile tile in tiles)
            {
                int tileId = _tileGrid.TileInfo.GetTile(tile).Id;
                
                foreach(Tile neighbour in tile.Neighbours(_tileGrid).Where(FilterTiles()))
                {
                    int neighbourId = _tileGrid.TileInfo.GetTile(neighbour).Id;
                    
                    if (!_aStar.HasPoint(neighbourId))
                    {
                        _aStar.AddPoint(_tileGrid.TileInfo.GetTile(neighbour).Id, neighbour.Position);
                    }
                    
                    _aStar.ConnectPoints(tileId, neighbourId);
                }
            }

            return this;
        }

        public Tile[] Find(Tile from, Tile to)
        {

            int[] ids = _aStar.GetIdPath(_tileGrid.TileInfo.GetTile(from).Id, _tileGrid.TileInfo.GetTile(to).Id);

            return ids.Select<int, Tile>(id => _tileGrid.TileInfo.GetTileById(id).Tile).ToArray();
        }
    }
}