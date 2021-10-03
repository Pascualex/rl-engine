using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

using System;

namespace RLEngine.Abilities
{
    public static class CombinedEffect
    {
        public static Log? CastCombined(this IEffect effect, TargetDB targetDB, GameState state)
        {
            var log = new CombinedLog(effect.IsParallel);
            foreach (var nestedEffect in effect.Effects)
            {
                log.Add(nestedEffect.Cast(targetDB, state));
            }
            return log;
        }
    }
}
