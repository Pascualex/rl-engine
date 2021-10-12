using RLEngine.Turns;
using RLEngine.Boards;
using RLEngine.Utils;

namespace RLEngine.State
{
    public class GameState
    {
        public GameState(Size boardSize, TileType defaultTileType)
        {
            TurnManager = new();
            Board = new(boardSize, defaultTileType);
        }

        public TurnManager TurnManager { get; }
        public Board Board { get; }
    }
}