using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

using System;

namespace RLEngine.Actions
{
    public static class HealAction
    {
        public static Log? Heal(this GameState state,
        Entity entity, Entity? healer, IActionAmount amount)
        {
            if (entity.IsDead) return null;
            if (healer?.IsDead ?? false) return null;
            var heal = amount.Calculate(healer);
            var actualHeal = entity.Heal(heal);
            return new HealLog(entity, healer, heal, actualHeal);
        }

        public static Log? Heal(this GameState state,
        Entity entity, IActionAmount amount)
        {
            return Heal(state, entity, null, amount);
        }
    }
}