using RLEngine.Logs;

namespace RLEngine.Events
{
    internal class DamageReactionEvent : ReactionEvent<DamageLog>
    {
        public DamageReactionEvent(DamageLog log)
        : base(log) { }

        protected override ILog? InternalInvoke(EventContext ctx)
        {
            if (actionLog.Target.Health == 0)
            {
                return ctx.ActionExecutor.Destroy(actionLog.Target);
            }

            return null;
        }
    }
}
