using RLEngine.Actions;

namespace RLEngine.Abilities
{
    public interface IHealingEffect
    {
        string Source { get; }
        string Target { get; }
        ActionAmount Amount { get; }
    }
}