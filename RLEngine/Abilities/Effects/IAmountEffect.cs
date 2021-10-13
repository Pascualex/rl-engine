using RLEngine.Actions;

namespace RLEngine.Abilities
{
    public interface IAmountEffect
    {
        string Target { get; }
        string Source { get; }
        ActionAmount Amount { get; }
    }
}