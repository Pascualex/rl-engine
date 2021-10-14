using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Utils;

using System;
using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public class Effect : ICombinedEffect, IAmountEffect, IProjectileEffect
    {
        public EffectType Type { get; init; } = EffectType.Unset;
        public bool IsParallel { get; init; } = false;
        public IEnumerable<Effect> Effects { get; init; } = new List<Effect>();
        public string Target { get; init; } = string.Empty;
        public string Source { get; init; } = string.Empty;
        public string From { get; init; } = string.Empty;
        public string To { get; init; } = string.Empty;
        public ActionAmount Amount { get; init; } = new();

        public Log? Cast(TargetDB targetDB, GameState state) => Type switch
        {
            EffectType.Combined => this.CastCombined(targetDB, state),
            EffectType.Damage => this.CastDamage(targetDB, state),
            EffectType.Destroy => this.CastDestroy(targetDB, state),
            EffectType.Heal => this.CastHeal(targetDB, state),
            EffectType.Projectile => this.Shoot(targetDB, state),
            _ => null,
        };

        public Type? GetEffectType() => Type switch
        {
            EffectType.Combined => typeof(ICombinedEffect),
            EffectType.Damage => typeof(IAmountEffect),
            EffectType.Destroy => typeof(ITargetEffect),
            EffectType.Heal => typeof(IAmountEffect),
            EffectType.Projectile => typeof(IProjectileEffect),
            _ => null,
        };
    }
}