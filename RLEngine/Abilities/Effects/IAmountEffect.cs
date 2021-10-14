using RLEngine.Actions;

namespace RLEngine.Abilities
{
    public interface IAmountEffect : ITargetEffect
    {
        string Source { get; }
        ActionAmount Amount { get; }
    }
}