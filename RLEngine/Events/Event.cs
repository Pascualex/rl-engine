using RLEngine.Logs;

using System;

namespace RLEngine.Events
{
    internal abstract class Event
    {
        private bool hasBeenInvoked = false;

        public ILog? Invoke(EventContext ctx)
        {
            if (hasBeenInvoked) throw new InvalidOperationException();
            hasBeenInvoked = true;
            return InternalInvoke(ctx);
        }

        protected abstract ILog? InternalInvoke(EventContext ctx);
    }
}