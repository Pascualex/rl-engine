using RLEngine.Abilities;
using RLEngine.Entities;

namespace RLEngine.Input
{
    public class AbilityInput : IPlayerInput
    {
        public AbilityInput(Ability ability, IEntity target)
        {
            Ability = ability;
            Target = target;
        }

        public Ability Ability { get; }
        public IEntity Target { get; }
    }
}