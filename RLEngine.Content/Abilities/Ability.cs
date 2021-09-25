using RLEngine.Abilities;

using System.Text.Json.Serialization;

namespace RLEngine.Content.Abilities
{
    public class Ability : IAbility
    {
        public int Cost { get; set; } = 0;
        [JsonIgnore]
        public IEffect Effect => SerializedEffect;

        [JsonPropertyName("Effect")]
        public Effect SerializedEffect { get; set; } = new();
    }
}