using Godot;
using TinyDungeon.World;

namespace TinyDungeon.Dungeon
{
    public class Wall : ITileable
    {
        public readonly Vector2 Position;

        public readonly Vector2 Size;

        public Wall(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }


        public Vector2 GetGridSize()
        {
            return new Vector2(Size.x, Size.y + 2);
        }

        public TileGrid GetGrid()
        {
            var grid = new TileGrid();

            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    var position = new Vector2(x, y);
                    
                    grid.SetTile(position, GetTile(position));
                }
            }

            return grid;
        }

        private TileType GetTile(Vector2 position)
        {
            var size = GetGridSize();
            
            if (IsEdge(position))
            {
                if (position.x == 0 && position.y == 0)
                {
                    return TileType.CORNER_WALL_NORTH_WEST;
                }

                if (position.x == 0 && position.y == size.y - 1)
                {
                    return TileType.CORNER_WALL_SOUTH_WEST;
                }

                if (position.x == 0 && position.y > 0)
                {
                    return TileType.WALL_WEST;
                }
            }

            return TileType.EMPTY;
        }

        private bool IsEdge(Vector2 position)
        {
            return position.x == 0 || position.y == 0 || position.x == GetGridSize().x - 1 ||
                   position.y == GetGridSize().y - 1;
        }
    }
}