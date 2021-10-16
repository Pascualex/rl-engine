using RLEngine.Abilities;
using RLEngine.Utils;

namespace RLEngine.Input
{
    public class SpellInput : PlayerInput
    {
        public SpellInput(Ability ability, Coords coords)
        {
            Ability = ability;
            Coords = coords;
        }

        public Ability Ability { get; }
        public Coords Coords { get; }
    }
}