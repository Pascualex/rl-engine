using RLEngine.Logs;

namespace RLEngine.Events
{
    internal abstract class ReactionEvent<T> : Event where T : Log
    {
        protected T actionLog;

        protected ReactionEvent(T actionLog, EventContext ctx)
        : base(ctx)
        {
            this.actionLog = actionLog;
        }
    }
}
