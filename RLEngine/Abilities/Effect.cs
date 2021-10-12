using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public class Effect
    {
        public EffectType Type { get; init; } = EffectType.Unset;
        public IEnumerable<Effect> Effects { get; init; } = new List<Effect>();
        public bool IsParallel { get; init; } = false;
        public ActionAmount Amount { get; init; } = new();
        public string Target { get; init; } = string.Empty;
        public string Source { get; init; } = string.Empty;

        public Log? Cast(TargetDB targetDB, GameState state) => Type switch
        {
            EffectType.Combined => this.CastCombined(targetDB, state),
            EffectType.Damage => this.CastDamage(targetDB, state),
            _ => null,
        };
    }
}