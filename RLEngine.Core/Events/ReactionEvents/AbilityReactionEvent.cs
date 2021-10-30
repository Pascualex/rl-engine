using RLEngine.Core.Logs;
using RLEngine.Core.Abilities;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System.Collections.Generic;

namespace RLEngine.Core.Events
{
    internal class AbilityReactionEvent : ReactionEvent<AbilityLog>
    {
        public AbilityReactionEvent(AbilityLog actionLog)
        : base(actionLog) { }

        protected override ILog? InternalInvoke(EventContext ctx)
        {
            var events = new List<Event>();
            var targetDB = new TargetDB();
            targetDB.Add("caster", actionLog.Caster);
            if (actionLog.Target is IEntity eTarget) targetDB.Add("target", eTarget);
            if (actionLog.Target is Coords cTarget) targetDB.Add("target", cTarget);
            foreach (var effect in actionLog.Ability.Effects)
            {
                events.Add(EffectEvent.FromEffect(effect, targetDB));
            }
            ctx.EventStack.Push(events);
            return null;
        }
    }
}
