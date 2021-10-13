using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using NRE = System.NullReferenceException;

namespace RLEngine.Abilities
{
    public static class HealEffectExtensions
    {
        public static Log? CastHeal(this IAmountEffect effect,
        TargetDB targetDB, GameState state)
        {
            if (effect.Amount is null) return null;

            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();
            var healer = targetDB.GetEntity(effect.Source);

            return state.Heal(target, healer, effect.Amount);
        }
    }
}
