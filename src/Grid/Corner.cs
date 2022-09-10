namespace TinyDungeon.Grid
{
    public class Corner
    {
        public readonly Tile Tile;

        public readonly Cell Cell;
        
        public Corner(Tile tile, Cell cell)
        {
            Tile = tile;
            Cell = cell;
        }
    }
}