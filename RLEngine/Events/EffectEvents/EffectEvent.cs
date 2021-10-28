using RLEngine.Abilities;

using System;

namespace RLEngine.Events
{
    internal abstract class EffectEvent : Event
    {
        protected EffectEvent()
        { }

        public static EffectEvent FromEffect(Effect effect, TargetDB targetDB)
        => effect.Type switch
        {
            EffectType. AreaTarget => new  AreaTargetEffectEvent(effect, targetDB),
            EffectType.     Damage => new      DamageEffectEvent(effect, targetDB),
            EffectType.Destruction => new DestructionEffectEvent(effect, targetDB),
            EffectType.      Group => new       GroupEffectEvent(effect, targetDB),
            EffectType.    Healing => new     HealingEffectEvent(effect, targetDB),
            EffectType. Projectile => new  ProjectileEffectEvent(effect, targetDB),
            _ => throw new InvalidOperationException(),
        };
    }

    internal abstract class EffectEvent<T> : EffectEvent where T : IEffect
    {
        protected T effect;
        protected TargetDB targetDB;

        protected EffectEvent(T effect, TargetDB targetDB)
        {
            this.effect = effect;
            this.targetDB = targetDB;
        }
    }
}
