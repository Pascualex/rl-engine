using RLEngine.Abilities;
using RLEngine.Entities;

namespace RLEngine.Input
{
    public class SpellInput : IPlayerInput
    {
        public SpellInput(Ability ability, Entity target)
        {
            Ability = ability;
            Target = target;
        }

        public Ability Ability { get; }
        public Entity Target { get; }
    }
}