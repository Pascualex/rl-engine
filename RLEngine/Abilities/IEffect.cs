using RLEngine.Actions;

using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public interface IEffect
    {
        EffectType Type { get; }
        IEnumerable<IEffect> Effects { get; }
        bool IsParallel { get; }
        ActionAmount Amount { get; }
    }
}