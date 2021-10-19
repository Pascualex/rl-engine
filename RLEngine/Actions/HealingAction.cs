using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Actions
{
    internal static class HealingAction
    {
        public static Log? Heal(this GameState state,
        Entity target, Entity? healer, ActionAmount amount)
        {
            if (target.IsDestroyed) return null;
            if (healer?.IsDestroyed ?? false) return null;
            var healing = amount.Calculate(target, healer);
            var actualHealing = target.Heal(healing);
            return new HealingLog(target, healer, healing, actualHealing);
        }

        public static Log? Heal(this GameState state,
        Entity target, ActionAmount amount)
        {
            return Heal(state, target, null, amount);
        }
    }
}