using RLEngine.Core.Turns;
using RLEngine.Core.Boards;

namespace RLEngine.Core.Actions
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