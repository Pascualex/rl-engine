using RLEngine.Logs;

namespace RLEngine.Events
{
    internal abstract class ReactionEvent : Event
    { }

    internal abstract class ReactionEvent<T> : ReactionEvent where T : ILog
    {
        protected T actionLog;

        protected ReactionEvent(T actionLog)
        {
            this.actionLog = actionLog;
        }
    }
}
