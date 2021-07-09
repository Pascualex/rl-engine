using RLEngine.Utils;

namespace  RLEngine.Boards
{
    public interface IReadOnlyBoard
    {
        Size Size { get; }

        IReadOnlyTile? GetTile(Coords at);
    }
}