using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Abilities
{
    public static class IAbilityExtensions
    {
        public static Log? Cast(this IAbility ability, Entity caster, Entity target, GameState state)
        {
            return ability.Effect.Cast(caster, target, state);
        }
    }
}
