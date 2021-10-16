using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public interface IGroupEffect
    {
        bool IsParallel { get; }
        IEnumerable<Effect> Effects { get; }
        string Group { get; }
        string NewTarget { get; }
    }
}