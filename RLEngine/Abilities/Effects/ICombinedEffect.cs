using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public interface ICombinedEffect
    {
        bool IsParallel { get; }
        IEnumerable<Effect> Effects { get; }
    }
}