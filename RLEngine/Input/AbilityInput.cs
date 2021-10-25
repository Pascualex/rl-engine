using RLEngine.Abilities;
using RLEngine.Entities;

namespace RLEngine.Input
{
    public class AbilityInput : IPlayerInput
    {
        public AbilityInput(Ability ability, Entity target)
        {
            Ability = ability;
            Target = target;
        }

        public Ability Ability { get; }
        public Entity Target { get; }
    }
}