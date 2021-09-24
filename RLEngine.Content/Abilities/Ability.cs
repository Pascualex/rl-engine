using RLEngine.Abilities;

using System.Collections.Generic;

namespace RLEngine.Content.Abilities
{
    public class Ability : IAbility
    {
        public int Cost { get; set; }
        public CombinedEffect RootEffect { get; set; } = new CombinedEffect(false);

        public void Add(Effect effect)
        {
            RootEffect.Add(effect);
        }
    }
}