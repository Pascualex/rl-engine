using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Abilities
{
    public static class IAbilityExtensions
    {
        public static Log Cast(this IAbility ability, Entity caster, Entity target, GameState state)
        {
            var combinedLog = new CombinedLog(false);

            foreach (var effect in ability.Effects)
            {
                combinedLog.Add(effect.Cast(caster, target, state));
            }

            return combinedLog;
        }
    }
}
