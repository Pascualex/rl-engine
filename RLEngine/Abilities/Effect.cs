using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using System;
using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public class Effect :
        IAreaTargetEffect,
        ICombinedEffect,
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
        public ActionAmount Amount { get; init; } = new();
        public int Radius { get; init; } = 0;
        public string NewGroup { get; init; } = string.Empty;
        public string NewTarget { get; init; } = string.Empty;
        public bool IsParallel { get; init; } = false;
        public IEnumerable<Effect> Effects { get; init; } = new List<Effect>();

        public Log? Cast(TargetDB targetDB, GameState state) => Type switch
        {
            EffectType. AreaTarget => this.    TargetArea(targetDB, state),
            EffectType.   Combined => this.CastSubeffects(targetDB, state),
            EffectType.     Damage => this.        Damage(targetDB, state),
            EffectType.Destruction => this.       Destroy(targetDB, state),
            EffectType.      Group => this.     CastGroup(targetDB, state),
            EffectType.    Healing => this.          Heal(targetDB, state),
            EffectType. Projectile => this.         Shoot(targetDB, state),
            _ => null,
        };

        public Type? GetEffectType() => Type switch
        {
            EffectType. AreaTarget => typeof( IAreaTargetEffect),
            EffectType.   Combined => typeof(   ICombinedEffect),
            EffectType.     Damage => typeof(     IDamageEffect),
            EffectType.Destruction => typeof(IDestructionEffect),
            EffectType.      Group => typeof(      IGroupEffect),
            EffectType.    Healing => typeof(    IHealingEffect),
            EffectType. Projectile => typeof( IProjectileEffect),
            _ => null,
        };
    }
}