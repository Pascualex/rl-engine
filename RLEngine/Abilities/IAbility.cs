using RLEngine.Utils;

namespace RLEngine.Abilities
{
    public interface IAbility : IIdentifiable
    {
        AbilityType Type { get; }
        int Cost { get; }
        IEffect Effect { get; }
    }
}
