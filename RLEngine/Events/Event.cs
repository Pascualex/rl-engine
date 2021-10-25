using RLEngine.Logs;

using System;
using System.Collections.Generic;

namespace RLEngine.Events
{
    internal abstract class Event
    {
        protected EventContext ctx;
        private bool isInvoked = false;

        protected Event(EventContext ctx)
        {
            this.ctx = ctx;
        }

        public Log? Invoke()
        {
            if (isInvoked) throw new InvalidOperationException();
            isInvoked = true;

            return InternalInvoke();
        }

        protected abstract Log? InternalInvoke();
    }
}