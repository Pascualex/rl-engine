using RLEngine.Entities;

namespace RLEngine.Abilities
{
    public interface IDamageEffect : IEffect
    {
        string Source { get; }
        string Target { get; }
        Amount Amount { get; }
    }
}