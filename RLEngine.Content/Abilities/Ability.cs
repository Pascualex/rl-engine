using RLEngine.Abilities;

using System.Collections.Generic;

namespace RLEngine.Content.Abilities
{
    public class Ability : IAbility
    {
        private readonly List<IEffect> effects = new();

        public int Cost { get; set; }
        public IEnumerable<IEffect> Effects => effects;

        public void Add(IEffect effect)
        {
            effects.Add(effect);
        }
    }
}