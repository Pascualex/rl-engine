using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Actions
{
    public static class HealAction
    {
        public static Log? Heal(this GameState state,
        Entity target, Entity? healer, ActionAmount amount)
        {
            if (target.IsDead) return null;
            if (healer?.IsDead ?? false) return null;
            var healing = amount.Calculate(healer);
            var actualHealing = target.Heal(healing);
            return new HealLog(target, healer, healing, actualHealing);
        }

        public static Log? Heal(this GameState state,
        Entity target, ActionAmount amount)
        {
            return Heal(state, target, null, amount);
        }
    }
}