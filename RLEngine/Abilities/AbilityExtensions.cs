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
            return ability.Effect.Cast(targetDB, state);
        }
    }
}
