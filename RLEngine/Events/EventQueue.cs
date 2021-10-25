using RLEngine.Logs;

using System.Collections.Generic;
using System.Linq;

namespace RLEngine.Events
{
    internal class EventQueue
    {
        private readonly LinkedList<Event> eventsQueue = new();

        public int Count => eventsQueue.Count;

        public Log? Invoke()
        {
            var evt = eventsQueue.First.Value;
            eventsQueue.RemoveFirst();
            return evt.Invoke();
        }

        public void AddFirst(Event evt)
        {
            eventsQueue.AddFirst(evt);
        }

        public void AddFirst(IEnumerable<Event> events)
        {
            foreach (var evt in events.Reverse())
            {
                eventsQueue.AddFirst(evt);
            }
        }

        public void AddLast(Event evt)
        {
            eventsQueue.AddLast(evt);
        }
    }
}