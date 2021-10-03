using RLEngine.Serialization.Utils;

using RLEngine.Abilities;

using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Abilities
{
    public class Ability : Deserializable, IAbility
    {
        public AbilityType Type { get; set; } = AbilityType.SelfTarget;
        public int Cost { get; set; } = 0;
        [YamlMember(SerializeAs = typeof(Effect))]
        public IEffect Effect { get; set; } = new Effect();
    }
}