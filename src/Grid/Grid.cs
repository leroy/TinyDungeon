using Godot;

namespace TinyDungeon.Grid
{
    public class Grid
    {
        private RandomNumberGenerator _randomNumberGenerator;
        
        public readonly int Width;

        public readonly int Height;

        public int RealWidth => Width * 2 + 2;
        
        public int RealHeight => Height * 2 + 2; 
        
        public Grid(int width, int height)
        {
            Width = width;
            Height = height;

            _randomNumberGenerator = new RandomNumberGenerator();
        }

        public Tile GetRandomTile()
        {
            int x = _randomNumberGenerator.RandiRange(0, Width);
            int y = _randomNumberGenerator.RandiRange(0, Height);

            return new Tile(Cell.FromTilePosition(new Vector2(x, y)));
        }
        
        public bool WithinBounds(Cell cell)
        {
            return cell.Position.x > 0 && cell.Position.x <= RealWidth && cell.Position.y > 0 && cell.Position.y <= RealHeight;
        }
    }
}