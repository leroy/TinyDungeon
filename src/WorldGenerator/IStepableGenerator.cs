namespace TinyDungeon.WorldGenerator
{
    public interface IStepableGenerator
    {


        void Step();
        bool IsFinished();
    }
}