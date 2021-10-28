﻿using RLEngine.Logs;
using RLEngine.Entities;

namespace RLEngine.Actions
{
    internal partial class ActionExecutor
    {
        public DamageLog? Damage(IEntity target, Amount amount)
        {
            return Damage(target, null, amount);
        }

        public DamageLog? Damage(IEntity target, IEntity? attacker, Amount amount)
        {
            if (target.IsDestroyed) return null;
            if (attacker?.IsDestroyed ?? false) return null;
            var damage = amount.Calculate(target, attacker);
            var actualDamage = target.Damage(damage);
            return new(target, attacker, damage, actualDamage);
        }
    }
}