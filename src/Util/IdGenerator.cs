namespace TinyDungeon.Util
{
    public class IdGenerator
    {
        private int _autoIncrement;

        public int Id()
        {
            return _autoIncrement++;
        }
    }
}