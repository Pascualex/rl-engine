using RLEngine.Logs;
using RLEngine.Entities;

namespace RLEngine.Actions
{
    internal partial class ActionExecutor
    {
        public HealingLog? Heal(IEntity target, Amount amount)
        {
            return Heal(target, null, amount);
        }

        public HealingLog? Heal(IEntity target, IEntity? healer, Amount amount)
        {
            if (target.IsDestroyed) return null;
            if (healer?.IsDestroyed ?? false) return null;
            var healing = amount.Calculate(target, healer);
            var actualHealing = target.Heal(healing);
            return new(target, healer, healing, actualHealing);
        }
    }
}