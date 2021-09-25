using RLEngine.Abilities;

using YamlDotNet.Serialization;

namespace RLEngine.Content.Abilities
{
    public class Ability : IAbility
    {
        public int Cost { get; set; } = 0;
        [YamlIgnore]
        public IEffect Effect => SerializedEffect;

        [YamlMember(Alias = "Effect")]
        public Effect SerializedEffect { get; set; } = new();
    }
}