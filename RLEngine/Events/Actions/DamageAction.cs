using RLEngine.Logs;
using RLEngine.Entities;

namespace RLEngine.Events
{
    internal static class DamageAction
    {
        public static DamageLog? Damage(this EventContext ctx,
        Entity target, ActionAmount amount)
        {
            return Damage(ctx, target, null, amount);
        }

        public static DamageLog? Damage(this EventContext ctx,
        Entity target, Entity? attacker, ActionAmount amount)
        {
            if (target.IsDestroyed) return null;
            if (attacker?.IsDestroyed ?? false) return null;
            var damage = amount.Calculate(target, attacker);
            var actualDamage = target.Damage(damage);
            var log = new DamageLog(target, attacker, damage, actualDamage);
            ctx.EventQueue.AddFirst(new DamageReactionEvent(log, ctx));
            return log;
        }
    }
}