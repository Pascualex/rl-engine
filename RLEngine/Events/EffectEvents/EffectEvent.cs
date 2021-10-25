using RLEngine.Abilities;

using System;

namespace RLEngine.Events
{
    internal abstract class EffectEvent : Event
    {
        protected EffectEvent(EventContext ctx)
        : base(ctx)
        { }

        public static EffectEvent FromEffect(Effect effect, TargetDB targetDB, EventContext ctx)
        => effect.Type switch
        {
            EffectType. AreaTarget => new  AreaTargetEffectEvent(effect, targetDB, ctx),
            EffectType.     Damage => new      DamageEffectEvent(effect, targetDB, ctx),
            EffectType.Destruction => new DestructionEffectEvent(effect, targetDB, ctx),
            EffectType.      Group => new       GroupEffectEvent(effect, targetDB, ctx),
            EffectType.    Healing => new     HealingEffectEvent(effect, targetDB, ctx),
            EffectType. Projectile => new  ProjectileEffectEvent(effect, targetDB, ctx),
            _ => throw new InvalidOperationException(),
        };
    }

    internal abstract class EffectEvent<T> : EffectEvent where T : IEffect
    {
        protected T effect;
        protected TargetDB targetDB;

        protected EffectEvent(T effect, TargetDB targetDB, EventContext ctx)
        : base(ctx)
        {
            this.effect = effect;
            this.targetDB = targetDB;
        }
    }
}
