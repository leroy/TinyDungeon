using System;
using System.Collections.Generic;
using Godot;

namespace TinyDungeon.Grid {
    public class Tile {
        public readonly Cell Cell;

        public Vector2 Position => new Vector2((int) (Cell.Position.x - 1) / 2, (int) (Cell.Position.y - 1) / 2);
        
        public Tile North => GetNeighbourByOffset(new Vector2(0, -1));
        public Tile West => GetNeighbourByOffset(new Vector2(-1, 0));
        public Tile East => GetNeighbourByOffset(new Vector2(1, 0));
        public Tile South => GetNeighbourByOffset(new Vector2(0, 1));

        public Tile[] Neighbours => new Tile[]
        {
            North,
            West,
            East,
            South
        };

        // Edges
        public Edge NorthEdge => new Edge(this, Cell.FromEdgePosition(this.Position, EdgeSide.NORTH), EdgeSide.NORTH);
        public Edge WestEdge => new Edge(this, Cell.FromEdgePosition(this.Position, EdgeSide.WEST), EdgeSide.WEST);
        public Edge SouthEdge => new Edge(South, Cell.FromEdgePosition(South.Position, EdgeSide.NORTH), EdgeSide.NORTH);
        public Edge EastEdge => new Edge(East, Cell.FromEdgePosition(East.Position, EdgeSide.WEST), EdgeSide.WEST);

        public Edge[] Edges => new Edge[]
        {
            NorthEdge,
            EastEdge,
            WestEdge,
            SouthEdge
        };
        
        public Dictionary<Tile, Edge> EdgeMap => new Dictionary<Tile, Edge>()
        {
            { North, NorthEdge },
            { West, WestEdge },
            { South, SouthEdge },
            { East, EastEdge }
        };
        
        // Corners
        public Corner NorthWestCorner => new Corner(this, Cell.FromCornerPosition(this.Position));
        public Corner NorthEastCorner => new Corner(this, Cell.FromCornerPosition(East.Position));
        public Corner SouthEastCorner => new Corner(this, Cell.FromCornerPosition(GetNeighbourByOffset(new Vector2(1, 1)).Position));
        public Corner SouthWestCorner => new Corner(South, Cell.FromCornerPosition(South.Position));


        public Corner[] Corners => new[]
        {
            NorthWestCorner,
            NorthEastCorner,
            SouthEastCorner,
            SouthWestCorner
        };
        
        public Tile(Cell cell)
        {
            this.Cell = cell;
        }
        
        public Edge EdgeBetween(Tile other)
        {
            Vector2 offset = other.Position - Position;

            if (offset.x > 1 || offset.y > 1)
            {
                throw new Exception("Given cell should be neighbouring cell");
            }

            return EdgeMap[GetNeighbourByOffset(offset)];
        }

        public Tile GetNeighbourByOffset(Vector2 offset)
        {
            return new Tile(Cell.FromTilePosition(Position + offset));
        }

        public override string ToString()
        {
            return $"{Position.x}-{Position.y}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Tile tile)
            {
                return tile.ToString() == this.ToString();
            }

            if (obj is String s)
            {
                return this.ToString() == s;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}