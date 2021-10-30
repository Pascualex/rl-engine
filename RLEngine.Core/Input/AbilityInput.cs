using RLEngine.Core.Abilities;
using RLEngine.Core.Entities;

namespace RLEngine.Core.Input
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