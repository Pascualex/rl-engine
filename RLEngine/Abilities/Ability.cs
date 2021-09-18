using RLEngine.Logs;
using RLEngine.Entities;

using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public class Ability
    {
        public List<IEffect> Effects { get; } = new();

        public Log Cast(Entity caster, Entity target)
        {
            var combinedLog = new CombinedLog(false);

            foreach (var effect in Effects)
            {
                combinedLog.Add(effect.Cast(caster, target));
            }

            return combinedLog;
        }
    }
}