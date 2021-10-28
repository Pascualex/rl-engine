using RLEngine.Logs;
using RLEngine.Abilities;

using NRE = System.NullReferenceException;

namespace RLEngine.Events
{
    internal class HealingEffectEvent : EffectEvent<IHealingEffect>
    {
        public HealingEffectEvent(IHealingEffect effect, TargetDB targetDB)
        : base(effect, targetDB) { }

        protected override ILog? InternalInvoke(EventContext ctx)
        {
            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();
            var healer = targetDB.GetEntity(effect.Source);
            return ctx.ActionExecutor.Heal(target, healer, effect.Amount);
        }
    }
}
