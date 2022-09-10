using System;
using System.Collections.Generic;

namespace TinyDungeon.World
{
    public class TileSet
    {
        private static Dictionary<TileType, TileSetTile> _data = new Dictionary<TileType, TileSetTile>()
        {
            { TileType.EMPTY, TileSetTile.Make(28)},
            
            { TileType.FLOOR, TileSetTile.Make(7) },
            { TileType.FLOOR_EDGE_NORTH, TileSetTile.Make(9) },
            { TileType.FLOOR_EDGE_NORTH_EAST, TileSetTile.Make(11) },
            { TileType.FLOOR_EDGE_EAST, TileSetTile.Make(9, false, false, true) },
            { TileType.FLOOR_EDGE_NORTH_WEST, TileSetTile.Make(11, true) },
            { TileType.FLOOR_EDGE_SOUTH_WEST, TileSetTile.Make(12) },
            { TileType.FLOOR_EDGE_SOUTH_EAST, TileSetTile.Make(12, true) },

            
            { TileType.WALL_LEDGE, TileSetTile.Make(26) },
            { TileType.WALL, TileSetTile.Make(27) },
            { TileType.INNER_WALL, TileSetTile.Make(28) },

            { TileType.WALL_EAST, TileSetTile.Make(20) },

            { TileType.WALL_WEST, TileSetTile.Make(24) },

            { TileType.INNER_WALL_SOUTH, TileSetTile.Make(22) },
            { TileType.INNER_WALL_SOUTH_WEST, TileSetTile.Make(21) },
            { TileType.INNER_WALL_SOUTH_EAST, TileSetTile.Make(23) },

            { TileType.INNER_WALL_NORTH, TileSetTile.Make(26) },
            { TileType.INNER_WALL_NORTH_WEST, TileSetTile.Make(19) },
            { TileType.INNER_WALL_NORTH_EAST, TileSetTile.Make(25) },


            { TileType.CORNER_WALL_NORTH_WEST, TileSetTile.Make(15) },
            { TileType.CORNER_WALL_NORTH_EAST, TileSetTile.Make(16) },
            { TileType.CORNER_WALL_SOUTH_WEST, TileSetTile.Make(17) },
            { TileType.CORNER_WALL_SOUTH_EAST, TileSetTile.Make(18) },
        };

        public static TileSetTile GetTileId(TileType tile)
        {
            if (!_data.ContainsKey(tile))
            {
                throw new ArgumentException($"{tile} not yet configured in TileTypeDictionary");
            }

            return _data[tile];
        }
    }
}