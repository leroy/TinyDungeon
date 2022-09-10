using Godot;

namespace TinyDungeon.World
{
    public interface ITileable
    {
        Vector2 GetGridSize();
        
        TileGrid GetGrid();
    }
}