using RLEngine.Boards;
using RLEngine.Utils;

namespace RLEngine.State
{
    public class GameState
    {
        public GameState(Size boardSize, ITileType defaultTileType)
        {
            Board = new(boardSize, defaultTileType);
        }

        public Board Board { get; }
    }
}