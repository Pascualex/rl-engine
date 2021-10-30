using RLEngine.Core.Entities;

using System;
using System.Collections.Generic;

namespace RLEngine.Core.Abilities
{
    public class Effect :
        IAreaTargetEffect,
        IDamageEffect,
        IDestructionEffect,
        IGroupEffect,
        IHealingEffect,
        IProjectileEffect
    {
        public EffectType Type { get; init; } = EffectType.Unset;
        public string Target { get; init; } = string.Empty;
        public string Source { get; init; } = string.Empty;
        public string Group { get; init; } = string.Empty;
        public Amount Amount { get; init; } = new();
        public int Radius { get; init; } = 0;
        public string NewGroup { get; init; } = string.Empty;
        public string NewTarget { get; init; } = string.Empty;
        public IEnumerable<Effect> Effects { get; init; } = new List<Effect>();

        public Type? GetEffectType() => Type switch
        {
            EffectType. AreaTarget => typeof( IAreaTargetEffect),
            EffectType.     Damage => typeof(     IDamageEffect),
            EffectType.Destruction => typeof(IDestructionEffect),
            EffectType.      Group => typeof(      IGroupEffect),
            EffectType.    Healing => typeof(    IHealingEffect),
            EffectType. Projectile => typeof( IProjectileEffect),
            _ => null,
        };
    }
}