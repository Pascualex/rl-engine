using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public class CombinedEffect : Effect
    {
        private readonly List<Effect> effects = new();

        public CombinedEffect(bool isParallel)
        {
            IsParallel = isParallel;
        }

        public bool IsParallel { get; }

        public override Log? Cast(Entity caster, Entity target, GameState state)
        {
            var log = new CombinedLog(IsParallel);

            foreach (var effect in effects)
            {
                log.Add(effect.Cast(caster, target, state));
            }

            return log;
        }

        public void Add(Effect effect)
        {
            effects.Add(effect);
        }
    }
}