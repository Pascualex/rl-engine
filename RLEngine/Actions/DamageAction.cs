using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Actions
{
    public static class DamageAction
    {
        public static Log? Damage(this GameState state,
        Entity target, Entity? attacker, ActionAmount amount)
        {
            if (target.IsDestroyed) return null;
            if (attacker?.IsDestroyed ?? false) return null;
            var damage = amount.Calculate(target, attacker);
            var actualDamage = target.Damage(damage);
            if (target.Health == 0)
            {
                var combinedLog = new CombinedLogBuilder(false);
                combinedLog.Add(new DamageLog(target, attacker, damage, actualDamage));
                combinedLog.Add(state.Destroy(target));
                return combinedLog.Build();
            }
            return new DamageLog(target, attacker, damage, actualDamage);
        }

        public static Log? Damage(this GameState state,
        Entity target, ActionAmount amount)
        {
            return Damage(state, target, null, amount);
        }
    }
}