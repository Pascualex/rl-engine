using RLEngine.Logs;
using RLEngine.Abilities;

using NRE = System.NullReferenceException;

namespace RLEngine.Events
{
    internal class DamageEffectEvent : EffectEvent<IDamageEffect>
    {
        public DamageEffectEvent(IDamageEffect effect, TargetDB targetDB, EventContext ctx)
        : base(effect, targetDB, ctx)
        { }

        protected override Log? InternalInvoke()
        {
            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();
            var attacker = targetDB.GetEntity(effect.Source);
            return ctx.Damage(target, attacker, effect.Amount);
        }
    }
}
