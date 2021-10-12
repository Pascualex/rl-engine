using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

using System;
using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public class Ability : IIdentifiable
    {
        public string ID { get; init; } = string.Empty;
        public int Cost { get; init; } = 0;
        public AbilityType Type { get; init; } = AbilityType.Unset;
        public IEnumerable<Effect> Effects { get; init; } = new List<Effect>();

        public Log? Cast(Entity caster, Entity target, GameState state)
        {
            if (Type != AbilityType.Entity) throw new InvalidOperationException();

            var targetDB = new TargetDB(caster, target);
            var log = new CombinedLog(false);
            foreach (var effect in Effects)
            {
                log.Add(effect.Cast(targetDB, state));
            }
            return log;
        }
    }
}
