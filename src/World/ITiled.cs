using Godot;

namespace TinyDungeon.World
{
    public interface ITiled
    {
        TileGrid Tiles
        {
            get;
        }

        Vector2 Size
        {
            get;
        }
        
    }
}