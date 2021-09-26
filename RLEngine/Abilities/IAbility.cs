using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public interface IAbility
    {
        AbilityType Type { get; }
        int Cost { get; }
        IEffect Effect { get; }
    }
}
