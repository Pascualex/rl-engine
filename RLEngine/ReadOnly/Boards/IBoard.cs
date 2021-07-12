using RLEngine.Utils;

namespace RLEngine.Boards
{
    public interface IBoard
    {
        Size Size { get; }

        ITile? GetTile(Coords at);
    }
}