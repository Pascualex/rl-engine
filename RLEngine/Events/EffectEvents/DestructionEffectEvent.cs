using RLEngine.Logs;
using RLEngine.Abilities;

using NRE = System.NullReferenceException;

namespace RLEngine.Events
{
    internal class DestructionEffectEvent : EffectEvent<IDestructionEffect>
    {
        public DestructionEffectEvent(IDestructionEffect effect,
        TargetDB targetDB, EventContext ctx) : base(effect, targetDB, ctx)
        { }

        protected override Log? InternalInvoke()
        {
            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();
            return ctx.Destroy(target);
        }
    }
}
