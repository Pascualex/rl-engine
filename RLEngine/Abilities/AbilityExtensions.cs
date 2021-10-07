using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

using System;

namespace RLEngine.Abilities
{
    public static class AbilityExtensions
    {
        public static Log? Cast(this IAbility ability, Entity caster, Entity target, GameState state)
        {
            if (ability.TargetType != TargetType.Entity) throw new InvalidOperationException();

            var targetDB = new TargetDB(caster, target);
            var log = new CombinedLog(false);
            foreach (var effect in ability.Effects)
            {
                log.Add(effect.Cast(targetDB, state));
            }
            return log;
        }
    }
}
