using RLEngine.Core.Entities;

namespace RLEngine.Core.Abilities
{
    public interface IHealingEffect : IEffect
    {
        string Source { get; }
        string Target { get; }
        Amount Amount { get; }
    }
}