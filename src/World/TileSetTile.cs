namespace TinyDungeon.World
{
    public class TileSetTile
    {
        public readonly int Id;

        public readonly bool FlipX;

        public readonly bool FlipY;

        public readonly bool Transpose;
        
        public TileSetTile(int id, bool flipX, bool flipY, bool transpose)
        {
            Id = id;
            FlipX = flipX;
            FlipY = flipY;
            Transpose = transpose;
        }

        public static TileSetTile Make(int id, bool flipX = false, bool flipY = false, bool transpose = false)
        {
            return new TileSetTile(id, flipX, flipY, transpose);
        }
    }
}