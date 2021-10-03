using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using NRE = System.NullReferenceException;

namespace RLEngine.Abilities
{
    public static class DamageEffect
    {
        public static Log? CastDamage(this IEffect effect, TargetDB targetDB, GameState state)
        {
            if (effect.Amount is null) return null;

            if (!targetDB.TryGetTarget(effect.Target, out var target)) throw new NRE();
            if (!targetDB.TryGetTarget(effect.Source, out var attacker)) throw new NRE();

            return state.Damage(target, attacker, effect.Amount);
        }
    }
}
