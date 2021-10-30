using RLEngine.Core.Logs;

namespace RLEngine.Core.Events
{
    internal class EventTriggerer
    {
        private readonly EventStack eventStack;

        public EventTriggerer(EventStack eventStack)
        {
            this.eventStack = eventStack;
        }

        public void Handle(ILog log)
        {
            var reactionEvent = GetReactionEvent(log);
            if (reactionEvent == null) return;
            eventStack.Push(reactionEvent);
        }

        private ReactionEvent? GetReactionEvent(ILog baseLog) => baseLog switch
        {
            AbilityLog log => new AbilityReactionEvent(log),
            DamageLog  log => new DamageReactionEvent( log),
            _ => null,
        };
    }
}