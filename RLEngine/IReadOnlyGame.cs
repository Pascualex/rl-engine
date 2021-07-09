using RLEngine.Boards;

namespace  RLEngine
{
    public interface IReadOnlyGame
    {
        IReadOnlyBoard Board { get; }
    }
}