using System;
using Godot;

namespace TinyDungeon
{
    public class Cell
    {
        public readonly Vector2 Position;

        public readonly CellType Type;

        public Cell(Vector2 position, CellType type)
        {
            Position = position;
            Type = type;
        }

        public override string ToString()
        {
            return $"{Position.x}-{Position.y}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Cell other)
            {
                return other.ToString() == this.ToString();
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

        public static Cell FromTilePosition(Vector2 position)
        {
            return new Cell(new Vector2(2 * position.x + 1, 2 * position.y + 1), CellType.TILE);
        }

        public static Cell FromEdgePosition(Vector2 position, EdgeSide side)
        {
            if (side == EdgeSide.NORTH)
            {
                return new Cell(new Vector2(position.x * 2 + 1, position.y * 2), CellType.EDGE);
            }

            if (side == EdgeSide.WEST)
            {
                return new Cell(new Vector2(position.x * 2, position.y * 2 + 1), CellType.EDGE);
            }

            throw new ArgumentException($"Unknown side {side}");
        }
        
        public static Cell FromCornerPosition(Vector2 position)
        {
            return new Cell(new Vector2(position.x * 2, position.y * 2), CellType.CORNER);
        }
    }
}