using RLEngine.Turns;
using RLEngine.Boards;

namespace RLEngine.Actions
{
    internal partial class ActionExecutor
    {
        protected TurnManager turnManager;
        protected IBoard board;

        public ActionExecutor(TurnManager turnManager, IBoard board)
        {
            this.turnManager = turnManager;
            this.board = board;
        }
    }
}