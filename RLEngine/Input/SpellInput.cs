using RLEngine.Abilities;
using RLEngine.Utils;

namespace RLEngine.Input
{
    public class AbilityInput : PlayerInput
    {
        public IAbility Ability { get; }
        public Coords Coords { get; }

        public AbilityInput(IAbility ability, Coords coords)
        {
            Ability = ability;
            Coords = coords;
        }
    }
}