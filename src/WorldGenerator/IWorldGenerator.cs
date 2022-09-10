using TinyDungeon.World;

namespace TinyDungeon.WorldGenerator
{
    public interface IWorldGenerator
    {
        void Reset();
        TileGrid GetResults();
        
        void Generate();
    }
}