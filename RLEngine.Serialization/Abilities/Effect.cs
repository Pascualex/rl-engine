using RLEngine.Abilities;
using RLEngine.Actions;

using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Abilities
{
    public class Effect : IEffect
    {
        public EffectType Type { get; set; } = EffectType.Combined;
        [YamlMember(SerializeAs = typeof(IList<Effect>))]
        public IEnumerable<IEffect> Effects { get; set; } = new List<Effect>();
        public bool IsParallel { get; set; } = false;
        public ActionAmount Amount { get; set; } = new();
    }
}