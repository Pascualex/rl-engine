using RLEngine.Events;

namespace RLEngine.Abilities
{
    public interface IHealingEffect : IEffect
    {
        string Source { get; }
        string Target { get; }
        ActionAmount Amount { get; }
    }
}