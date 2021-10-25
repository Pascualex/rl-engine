using RLEngine.Logs;
using RLEngine.Abilities;

using NRE = System.NullReferenceException;

namespace RLEngine.Events
{
    internal class HealingEffectEvent : EffectEvent<IHealingEffect>
    {
        public HealingEffectEvent(IHealingEffect effect, TargetDB targetDB, EventContext ctx)
        : base(effect, targetDB, ctx)
        { }

        protected override Log? InternalInvoke()
        {
            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();
            var healer = targetDB.GetEntity(effect.Source);
            return ctx.Heal(target, healer, effect.Amount);
        }
    }
}
