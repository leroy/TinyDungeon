using Godot;

namespace TinyDungeon.World
{
    public class TiledStructure<T> : ITiledStructure where T : ITiled
    {
        public readonly Vector2 Position;
        
        public readonly T Structure;

        public TiledStructure(Vector2 position, T structure)
        {
            Position = position;
            Structure = structure;
        }

        public Rect2 Bounds => new Rect2(Position, Structure.Size);

        public Vector2 Center => new Vector2(Position + Structure.Size / 2).Floor();

        public void MergeInto(TileGrid parent)
        {
            parent.Merge(Structure.Tiles, Position);
        }

        public Vector2 ToGlobalPosition(Vector2 local)
        {
            return Position + local;
        }
    }
}