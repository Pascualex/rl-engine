using RLEngine.Logs;

namespace RLEngine.Events
{
    internal class DamageReactionEvent : ReactionEvent<DamageLog>
    {
        public DamageReactionEvent(DamageLog actionLog, EventContext ctx)
        : base(actionLog, ctx)
        { }

        protected override Log? InternalInvoke()
        {
            if (actionLog.Target.Health == 0)
            {
                return ctx.Destroy(actionLog.Target);
            }

            return null;
        }
    }
}
