using Godot;
using System;
using System.Collections.Generic;

namespace TinyDungeon {
    public class Grid {
        private Vector2 _size;

        public Vector2 Size {
            get => _size;
        }

        public Grid(Vector2 size)
        {
            this._size = size;
        }

        public Vector2[] GetNeighbours(Vector2 cell)
        {
            List<Vector2> neighbours = new List<Vector2>() {};

            Vector2 left = cell + new Vector2(-1, 0);
            Vector2 right = cell + new Vector2(1, 0);
            Vector2 top = cell + new Vector2(0, -1);
            Vector2 bottom = cell + new Vector2(0, 1);


            foreach (Vector2 neighbour in new Vector2[] {
                left, right, top, bottom
            }) {
                if (IsWithinBounds(neighbour)) {
                    neighbours.Add(neighbour);
                }
            }

            return neighbours.ToArray();
        }

        public bool IsWithinBounds(Vector2 cell)
        {
            return cell.x > 0 && cell.x < _size.x && cell.y > 0 && cell.y < _size.y;
        }
    }
}