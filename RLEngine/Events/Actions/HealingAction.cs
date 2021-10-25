using RLEngine.Logs;
using RLEngine.Entities;

namespace RLEngine.Events
{
    internal static class HealingAction
    {
        public static HealingLog? Heal(this EventContext ctx,
        Entity target, ActionAmount amount)
        {
            return Heal(ctx, target, null, amount);
        }

        public static HealingLog? Heal(this EventContext ctx,
        Entity target, Entity? healer, ActionAmount amount)
        {
            if (target.IsDestroyed) return null;
            if (healer?.IsDestroyed ?? false) return null;
            var healing = amount.Calculate(target, healer);
            var actualHealing = target.Heal(healing);
            return new HealingLog(target, healer, healing, actualHealing);
        }
    }
}