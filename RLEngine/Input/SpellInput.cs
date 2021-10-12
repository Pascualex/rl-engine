using RLEngine.Abilities;
using RLEngine.Utils;

namespace RLEngine.Input
{
    public class AbilityInput : PlayerInput
    {
        public Ability Ability { get; }
        public Coords Coords { get; }

        public AbilityInput(Ability ability, Coords coords)
        {
            Ability = ability;
            Coords = coords;
        }
    }
}