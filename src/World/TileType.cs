namespace TinyDungeon.World
{
    public enum TileType
    {
        EMPTY,
        
        FLOOR,
        
        FLOOR_EDGE_NORTH,
        FLOOR_EDGE_NORTH_WEST,
        FLOOR_EDGE_NORTH_EAST,
        FLOOR_EDGE_EAST,
        FLOOR_EDGE_SOUTH_WEST,
        FLOOR_EDGE_SOUTH_EAST,
        
        EDGE_FLOOR_NORTH,
        EDGE_FLOOR_INNER_CORNER,
        EDGE_FLOOR_CORNER,
        
        WALL,
        WALL_LEDGE,
        INNER_WALL,

        WALL_WEST,
        WALL_EAST,

        INNER_WALL_NORTH,
        INNER_WALL_NORTH_WEST,
        INNER_WALL_NORTH_EAST,
        
        INNER_WALL_SOUTH,
        INNER_WALL_SOUTH_WEST,
        INNER_WALL_SOUTH_EAST,
        
        CORNER_WALL_NORTH_WEST,
        CORNER_WALL_NORTH_EAST,
        CORNER_WALL_SOUTH_WEST,
        CORNER_WALL_SOUTH_EAST,
    }
}