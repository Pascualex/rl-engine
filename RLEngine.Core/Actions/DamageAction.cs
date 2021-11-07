using RLEngine.Core.Logs;
using RLEngine.Core.Entities;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public DamageLog? Damage(IEntity target, Amount amount)
        {
            return Damage(target, null, amount);
        }

        public DamageLog? Damage(IEntity target, IEntity? attacker, Amount amount)
        {
            if (!target.IsActive) return null;
            if (!attacker?.IsActive ?? false) return null;
            var damage = amount.Calculate(target, attacker);
            var actualDamage = target.Damage(damage);
            return new(target, attacker, damage, actualDamage);
        }
    }
}