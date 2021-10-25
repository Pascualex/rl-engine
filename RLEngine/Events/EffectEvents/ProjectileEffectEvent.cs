using RLEngine.Logs;
using RLEngine.Abilities;

using NRE = System.NullReferenceException;

namespace RLEngine.Events
{
    internal class ProjectileEffectEvent : EffectEvent<IProjectileEffect>
    {
        public ProjectileEffectEvent(IProjectileEffect effect, TargetDB targetDB, EventContext ctx)
        : base(effect, targetDB, ctx)
        { }

        protected override Log? InternalInvoke()
        {
            var from = targetDB.GetCoords(effect.Source);
            var sourceE = targetDB.GetEntity(effect.Source);
            if (from == null && sourceE == null) throw new NRE();
            ITarget source = from != null ? new CoordsTarget(from) : new EntityTarget(sourceE!);

            var to = targetDB.GetCoords(effect.Target);
            var targetE = targetDB.GetEntity(effect.Target);
            if (to == null && targetE == null) throw new NRE();
            ITarget target = to != null ? new CoordsTarget(to) : new EntityTarget(targetE!);

            return ctx.Shoot(source, target);
        }
    }
}
