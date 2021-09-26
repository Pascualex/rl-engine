using RLEngine.Abilities;

namespace RLEngine.Content.Abilities
{
    public class Ability : IAbility
    {
        public AbilityType Type { get; set; } = AbilityType.None;
        public int Cost { get; set; } = 0;
        public IEffect Effect => SerializedEffect;

        public Effect SerializedEffect { get; set; } = new();
    }
}