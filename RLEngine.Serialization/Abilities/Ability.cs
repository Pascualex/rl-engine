using RLEngine.Serialization.Utils;

using RLEngine.Abilities;

using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Abilities
{
    public class Ability : IAbility, IDeserializable
    {
        [YamlIgnore]
        public string ID { get; set; } = "NO_ID";
        public AbilityType Type { get; set; } = AbilityType.Target;
        public int Cost { get; set; } = 0;
        [YamlIgnore]
        public IEffect Effect => SerializedEffect;

        [YamlMember(Alias = "Effect")]
        public Effect SerializedEffect { get; set; } = new();
    }
}