using RLEngine.Core.Entities;

namespace RLEngine.Core.Abilities
{
    public interface IDamageEffect : IEffect
    {
        string Source { get; }
        string Target { get; }
        IAmount Amount { get; }
    }
}