using RLEngine.Serialization.Utils;

using RLEngine.Abilities;

namespace RLEngine.Serialization.Abilities
{
    public class Ability : IAbility, IDeserializable
    {
        public string ID { get; set; } = "";
        public AbilityType Type { get; set; } = AbilityType.None;
        public int Cost { get; set; } = 0;
        public IEffect Effect => SerializedEffect;

        public Effect SerializedEffect { get; set; } = new();
    }
}