using RLEngine.Core.Logs;
using RLEngine.Core.Abilities;

using System.Collections.Generic;

namespace RLEngine.Core.Events
{
    internal class GroupEffectEvent : EffectEvent<IGroupEffect>
    {
        public GroupEffectEvent(IGroupEffect effect, TargetDB targetDB)
        : base(effect, targetDB)
        { }

        protected override ILog? InternalInvoke(EventContext ctx)
        {
            var events = new List<Event>();
            foreach (var target in targetDB.GetEntityGroup(effect.Group))
            {
                events.Add(new GroupEffectIteratorEvent(effect.NewTarget, target, targetDB));
                foreach (var effect in effect.Effects)
                {
                    events.Add(FromEffect(effect, targetDB));
                }
            }
            ctx.EventStack.Push(events);
            return null;
        }
    }
}
