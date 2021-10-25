using RLEngine.Logs;
using RLEngine.Abilities;

using System.Collections.Generic;

namespace RLEngine.Events
{
    internal class GroupEffectEvent : EffectEvent<IGroupEffect>
    {
        public GroupEffectEvent(IGroupEffect effect, TargetDB targetDB, EventContext ctx)
        : base(effect, targetDB, ctx)
        { }

        protected override Log? InternalInvoke()
        {
            var events = new List<Event>();
            foreach (var target in targetDB.GetEntityGroup(effect.Group))
            {
                events.Add(new GroupEffectIteratorEvent(effect.NewTarget, target, targetDB, ctx));
                foreach (var effect in effect.Effects)
                {
                    events.Add(EffectEvent.FromEffect(effect, targetDB, ctx));
                }
            }
            ctx.EventQueue.AddFirst(events);
            return null;
        }
    }
}
