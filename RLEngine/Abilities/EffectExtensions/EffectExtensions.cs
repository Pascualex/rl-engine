using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

using System;

namespace RLEngine.Abilities
{
    public static class EffectExtensions
    {
        public static Log? Cast(this IEffect effect, TargetDB targetDB, GameState state)
        => effect.Type switch
        {
            EffectType.Combined => effect.CastCombined(targetDB, state),
            EffectType.Damage => effect.CastDamage(targetDB, state),
            _ => null,
        };
    }
}
