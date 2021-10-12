using RLEngine.Actions;

namespace RLEngine.Abilities
{
    public interface IDamageEffect
    {
        string Target { get; }
        string Source { get; }
        ActionAmount Amount { get; }
    }
}