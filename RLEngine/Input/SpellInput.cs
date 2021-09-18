using RLEngine.Abilities;

namespace RLEngine.Input
{
    public class AbilityInput : PlayerInput
    {
        public IAbility Ability { get; }

        public AbilityInput(IAbility ability)
        {
            Ability = ability;
        }
    }
}