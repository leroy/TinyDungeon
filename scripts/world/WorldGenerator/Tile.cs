using System;
using Godot;

namespace TinyDungeon {
    public class Tile {
        public readonly TileType Type;

        public readonly Vector2 Position;

        public Tile(TileType type, Vector2 position)
        {
            this.Type = type;
            this.Position = position;
        }
    }
}