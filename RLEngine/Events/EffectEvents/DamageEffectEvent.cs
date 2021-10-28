using RLEngine.Logs;
using RLEngine.Abilities;

using NRE = System.NullReferenceException;

namespace RLEngine.Events
{
    internal class DamageEffectEvent : EffectEvent<IDamageEffect>
    {
        public DamageEffectEvent(IDamageEffect effect, TargetDB targetDB)
        : base(effect, targetDB)
        { }

        protected override ILog? InternalInvoke(EventContext ctx)
        {
            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();
            var attacker = targetDB.GetEntity(effect.Source);
            return ctx.ActionExecutor.Damage(target, attacker, effect.Amount);
        }
    }
}
