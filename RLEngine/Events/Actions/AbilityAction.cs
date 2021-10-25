using RLEngine.Logs;
using RLEngine.Abilities;
using RLEngine.Entities;
using RLEngine.Utils;

using System;
using System.Collections.Generic;

namespace RLEngine.Events
{
    internal static class AbilityAction
    {
        public static AbilityLog? Cast(this EventContext ctx,
        Entity caster, Ability ability)
        {
            if (ability.Type != AbilityType.SelfTarget) throw new InvalidOperationException();
            if (caster.IsDestroyed) return null;
            AddEffectEvents(ability, new TargetDB(caster), ctx);
            return new AbilityLog(caster, ability, new SelfTarget());
        }

        public static AbilityLog? Cast(this EventContext ctx,
        Entity caster, Entity target, Ability ability)
        {
            if (ability.Type != AbilityType.EntityTarget) throw new InvalidOperationException();
            if (caster.IsDestroyed) return null;
            if (target.IsDestroyed) return null;
            AddEffectEvents(ability, new TargetDB(caster, target), ctx);
            return new AbilityLog(caster, ability, new EntityTarget(target));
        }

        public static AbilityLog? Cast(this EventContext ctx,
        Entity caster, Coords target, Ability ability)
        {
            if (ability.Type != AbilityType.EntityTarget) throw new InvalidOperationException();
            if (caster.IsDestroyed) return null;
            AddEffectEvents(ability, new TargetDB(caster, target), ctx);
            return new AbilityLog(caster, ability, new CoordsTarget(target));
        }

        private static void AddEffectEvents(Ability ability, TargetDB targetDB, EventContext ctx)
        {
            foreach (var effect in ability.Effects)
            {
                ctx.EventQueue.AddLast(EffectEvent.FromEffect(effect, targetDB, ctx));
            }
        }
    }
}