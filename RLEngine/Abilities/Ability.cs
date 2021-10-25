using RLEngine.Utils;

using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public class Ability : IIdentifiable
    {
        public string ID { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public int Cost { get; init; } = 0;
        public AbilityType Type { get; init; } = AbilityType.Unset;
        public IEnumerable<Effect> Effects { get; init; } = new List<Effect>();
    }
}
