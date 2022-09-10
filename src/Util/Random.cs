using System.Collections;
using Godot;

namespace TinyDungeon.Util
{
    public class Random
    {
        private static RandomNumberGenerator _randomNumberGenerator = new RandomNumberGenerator();

        public static Vector2 RandomVector2I(Vector2 max, Vector2? min = null)
        {
            if (min == null)
            {
                min = Vector2.Zero;
            }

            int x = _randomNumberGenerator.RandiRange((int)((Vector2)min).x, (int)max.x);
            int y = _randomNumberGenerator.RandiRange((int)((Vector2)min).y, (int)max.y);

            return new Vector2(x, y);
        }

        public static int RandomIndex(ICollection collection)
        {
            return _randomNumberGenerator.RandiRange(0, collection.Count - 1);
        }
    }
}