using RLEngine.Serialization.Utils;

using RLEngine.Abilities;

using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Abilities
{
    public class Ability : Deserializable, IAbility
    {
        public int Cost { get; set; } = 0;
        public TargetType TargetType { get; set; } = TargetType.Self;
        [YamlMember(SerializeAs = typeof(Effect))]
        public IEffect Effect { get; set; } = new Effect();
    }
}