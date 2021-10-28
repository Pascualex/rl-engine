using RLEngine.Actions;
using RLEngine.Turns;
using RLEngine.Boards;

namespace RLEngine.Events
{
    internal class EventContext
    {
        public EventContext(EventStack eventStack, ActionExecutor actionExecutor,
        TurnManager turnManager, IBoard board)
        {
            ActionExecutor = actionExecutor;
            EventStack = eventStack;
            TurnManager = turnManager;
            Board = board;
        }

        public ActionExecutor ActionExecutor { get; }
        public EventStack EventStack { get; }
        public TurnManager TurnManager { get; }
        public IBoard Board { get; }
    }
}