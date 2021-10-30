using RLEngine.Core.Logs;
using RLEngine.Core.Abilities;
using RLEngine.Core.Utils;

using NRE = System.NullReferenceException;

namespace RLEngine.Core.Events
{
    internal class ProjectileEffectEvent : EffectEvent<IProjectileEffect>
    {
        public ProjectileEffectEvent(IProjectileEffect effect, TargetDB targetDB)
        : base(effect, targetDB) { }

        protected override ILog? InternalInvoke(EventContext ctx)
        {
            var from = targetDB.GetCoords(effect.Source);
            var sourceE = targetDB.GetEntity(effect.Source);
            var source = (ITarget?)from ?? sourceE ?? throw new NRE();

            var to = targetDB.GetCoords(effect.Target);
            var targetE = targetDB.GetEntity(effect.Target);
            var target = (ITarget?)to ?? targetE ?? throw new NRE();

            return ctx.ActionExecutor.Shoot(source, target);
        }
    }
}
