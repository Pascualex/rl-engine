using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.Turns;
using RLEngine.Boards;

using System.Collections.Generic;
using System.Linq;

namespace RLEngine.Events
{
    internal class EventStack
    {
        private readonly Stack<Event> eventStack = new();
        private readonly EventContext eventContext;

        public int Count => eventStack.Count;

        public EventStack(ActionExecutor actionExecutor, TurnManager turnManager, IBoard board)
        {
            eventContext = new EventContext(this, actionExecutor, turnManager, board);
        }

        public ILog? InvokeNext()
        {
            return eventStack.Pop().Invoke(eventContext);
        }

        public void Push(Event evt)
        {
            eventStack.Push(evt);
        }

        public void Push(IEnumerable<Event> events)
        {
            foreach (var evt in events.Reverse())
            {
                eventStack.Push(evt);
            }
        }
    }
}