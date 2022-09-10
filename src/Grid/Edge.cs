namespace TinyDungeon.Grid
{
    public class Edge
    {
        public readonly Tile Tile;

        public readonly Cell Cell;

        public readonly EdgeSide Side;

        public Edge(Tile tile, Cell cell, EdgeSide side)
        {
            Tile = tile;
            Cell = cell;
            Side = side;
        }

        public override string ToString()
        {
            return $"{Tile.Position.x}-{Tile.Position.y}-{Side.ToString()}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Edge edge)
            {
                return edge.ToString() == this.ToString();
            }
            
            if (obj is string s)
            {
                return s == this.ToString();
            }
            
            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}