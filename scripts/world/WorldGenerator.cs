using Godot;
using System;

namespace TinyDungeon {
    public class WorldGenerator : Resource {
        public WorldGenerator()
        {
            
        }

        public TileGrid Generate()
        {
            return new TileGrid(new Grid(new Vector2(10, 10)));
        }
    }
}