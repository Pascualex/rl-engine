namespace RLEngine.Core.Abilities
{
    public interface IDestructionEffect : IEffect
    {
        string Target { get; }
    }
}