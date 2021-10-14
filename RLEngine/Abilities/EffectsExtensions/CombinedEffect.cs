using RLEngine.Logs;
using RLEngine.State;

namespace RLEngine.Abilities
{
    public static class CombinedEffect
    {
        public static Log? CastSubeffects(this ICombinedEffect effect,
        TargetDB targetDB, GameState state)
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
