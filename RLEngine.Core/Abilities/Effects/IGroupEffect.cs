using System.Collections.Generic;

namespace RLEngine.Core.Abilities
{
    public interface IGroupEffect : IEffect
    {
        string Group { get; }
        string NewTarget { get; }
        IEnumerable<Effect> Effects { get; }
    }
}