using RLEngine.Abilities;
using RLEngine.Actions;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RLEngine.Content.Abilities
{
    public class Effect : IEffect
    {
        public EffectType Type { get; set; } = EffectType.Combined;
        [JsonIgnore]
        public IEnumerable<IEffect>? Effects => SerializedEffects;
        public bool IsParallel { get; set; } = false;
        public ActionAmount? Amount { get; set; } = null;

        [JsonPropertyName("Effects")]
        public List<Effect>? SerializedEffects { get; set; } = null;
    }
}