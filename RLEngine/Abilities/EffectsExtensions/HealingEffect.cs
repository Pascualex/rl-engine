using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using NRE = System.NullReferenceException;

namespace RLEngine.Abilities
{
    public static class HealingEffect
    {
        public static Log? Heal(this IHealingEffect effect,
        TargetDB targetDB, GameState state)
        {
            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();
            var healer = targetDB.GetEntity(effect.Source);

            return state.Heal(target, healer, effect.Amount);
        }
    }
}
