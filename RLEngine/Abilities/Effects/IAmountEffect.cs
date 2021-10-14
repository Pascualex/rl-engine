using RLEngine.Actions;

namespace RLEngine.Abilities
{
    public interface IAmountEffect : ISourceEffect
    {
        ActionAmount Amount { get; }
    }
}