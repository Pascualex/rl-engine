using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using System;
using NRE = System.NullReferenceException;

namespace RLEngine.Abilities
{
    public static class DamageEffectExtensions
    {
        public static Log? CastDamage(this IAmountEffect effect,
        TargetDB targetDB, GameState state)
        {
            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();
            var attacker = targetDB.GetEntity(effect.Source);

            return state.Damage(target, attacker, effect.Amount);
        }
    }
}
