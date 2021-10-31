using RLEngine.Core.Actions;
using RLEngine.Core.Logs;
using RLEngine.Core.Turns;
using RLEngine.Core.Boards;

using System.Collections.Generic;
using System.Linq;

namespace RLEngine.Core.Events
{
    internal class EventStack
    {
        private readonly Stack<Event> eventStack = new();
        private readonly EventContext eventContext;

        public int Count => eventStack.Count;

        public EventStack(ActionExecutor actionExecutor, ITurnManager turnManager, IBoard board)
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