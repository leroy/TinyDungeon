using System.Data.Common;
using System.IO;

namespace TinyDungeon.World
{
    public class TileInfo
    {
        public readonly int Id;

        public readonly Tile Tile;

        public TileInfo(int id, Tile tile)
        {
            Id = id;
            Tile = tile;
        }
    }
}