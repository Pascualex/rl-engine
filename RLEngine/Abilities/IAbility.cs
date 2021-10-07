using RLEngine.Utils;

using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public interface IAbility : IIdentifiable
    {
        int Cost { get; }
        TargetType TargetType { get; }
        IEnumerable<IEffect> Effects { get; }
    }
}
