using RLEngine.Boards;

namespace RLEngine
{
    public interface IGame
    {
        IBoard Board { get; }
    }
}