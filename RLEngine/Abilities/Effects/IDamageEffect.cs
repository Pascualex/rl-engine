using RLEngine.Events;

namespace RLEngine.Abilities
{
    public interface IDamageEffect : IEffect
    {
        string Source { get; }
        string Target { get; }
        ActionAmount Amount { get; }
    }
}