using RLEngine.Entities;

namespace RLEngine.Abilities
{
    public interface IHealingEffect : IEffect
    {
        string Source { get; }
        string Target { get; }
        Amount Amount { get; }
    }
}