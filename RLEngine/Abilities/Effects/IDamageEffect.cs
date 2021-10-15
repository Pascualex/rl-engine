using RLEngine.Actions;

namespace RLEngine.Abilities
{
    public interface IDamageEffect
    {
        string Source { get; }
        string Target { get; }
        ActionAmount Amount { get; }
    }
}