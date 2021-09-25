using System.Collections.Generic;

namespace RLEngine.Abilities
{
    public interface IAbility
    {
        int Cost { get; }
        IEffect Effect { get; }
    }
}
