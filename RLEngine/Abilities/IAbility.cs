using RLEngine.Utils;

namespace RLEngine.Abilities
{
    public interface IAbility : IIdentifiable
    {
        int Cost { get; }
        TargetType TargetType { get; }
        IEffect Effect { get; }
    }
}
