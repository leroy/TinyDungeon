using Godot;

namespace TinyDungeon.World
{
    public interface ITiledStructure
    {
        Rect2 Bounds
        {
            get;
        }

        void MergeInto(TileGrid parent);

        Vector2 ToGlobalPosition(Vector2 position);
    }
}