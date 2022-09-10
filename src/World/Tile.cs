using Godot;

namespace TinyDungeon.World
{
    public class Tile
    {
        public Vector2 Position { get; }
        public TileType Type { get; }

        public Tile(Vector2 position, TileType type)
        {
            Position = position;
            Type = type;
        }

        public TileNeighbours Neighbours(TileGrid grid)
        {
            return new TileNeighbours(grid, this);
        }

        public override string ToString()
        {
            return $"{Position.x}-${Position.y}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}