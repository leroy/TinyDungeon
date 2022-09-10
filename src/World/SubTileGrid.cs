namespace TinyDungeon.World
{
    public class SubTileGrid : TileGrid
    {
        private TileGrid _parent;
        
        public void SetParent(TileGrid parent)
        {
            _parent = parent;
        }
    }
}