using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using NRE = System.NullReferenceException;

namespace RLEngine.Abilities
{
    public static class DamageEffectExtensions
    {
        public static Log? CastDamage(this IDamageEffect effect,
        TargetDB targetDB, GameState state)
        {
            if (effect.Amount is null) return null;

            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();
            var attacker = targetDB.GetEntity(effect.Source);

            return state.Damage(target, attacker, effect.Amount);
        }
    }
}
