using RLEngine.Core.Turns;
using RLEngine.Core.Boards;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        protected ITurnManager turnManager;
        protected IBoard board;

        public ActionExecutor(ITurnManager turnManager, IBoard board)
        {
            this.turnManager = turnManager;
            this.board = board;
        }
    }
}