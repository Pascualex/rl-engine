using RLEngine.Core.Logs;
using RLEngine.Core.Entities;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public HealingLog? Heal(IEntity target, Amount amount)
        {
            return Heal(target, null, amount);
        }

        public HealingLog? Heal(IEntity target, IEntity? healer, Amount amount)
        {
            if (!target.IsActive) return null;
            if (!healer?.IsActive ?? false) return null;
            var healing = amount.Calculate(target, healer);
            var actualHealing = target.Heal(healing);
            return new(target, healer, healing, actualHealing);
        }
    }
}