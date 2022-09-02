using Godot;
using System;

namespace TinyDungeon {
    public class TileGrid {
        Grid grid;

        TileType[][] tiles;

        public TileGrid(Grid grid)
        {
            this.grid = grid;

            tiles = new TileType[(int) grid.Size.x][];

            int index = 0;

            foreach(TileType[] column in tiles) {

                tiles[index] = new TileType[(int) grid.Size.y];

                for (int i = 0; i < grid.Size.y; i++) {
                    tiles[index][i] = TileType.WALL;
                }
                

                index++;
            }

        }


        public TileType GetTile(Vector2 cell)
        {
            return tiles[(int) cell.x][(int) cell.y];
        }

        public Tile[] GetTiles()
        {
            Tile[] tiles = new Tile[(int) (grid.Size.x * grid.Size.y)];

            for (int x = 0; x < this.tiles.Length; x++) {
                for (int y = 0; y < this.tiles[x].Length; y++) {
                    tiles[(int) ((x * grid.Size.y) + y)] = new Tile(this.tiles[x][y], new Vector2(x, y));
                }
            }

            return tiles;
        }
    }
}