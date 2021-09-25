using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

using System;

namespace RLEngine.Abilities
{
    public static class IEffectExtension
    {
        public static Log? Cast(this IEffect effect,
        Entity caster, Entity target, GameState state) => effect.Type switch
        {
            EffectType.Combined => CastCombinedEffect(effect, caster, target, state),
            EffectType.Damage => CastDamageEffect(effect, caster, target, state),
            _ => null,
        };

        private static Log? CastCombinedEffect(IEffect effect,
        Entity caster, Entity target, GameState state)
        {
            var log = new CombinedLog(effect.IsParallel);
            if (effect.Effects is null) return log;
            foreach (var nestedEffect in effect.Effects)
            {
                log.Add(nestedEffect.Cast(caster, target, state));
            }
            return log;
        }

        private static Log? CastDamageEffect(IEffect effect,
        Entity caster, Entity target, GameState state)
        {
            if (effect.Amount is null) return null;
            return state.Damage(target, caster, effect.Amount);
        }
    }
}
