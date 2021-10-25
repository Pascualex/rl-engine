namespace RLEngine.Abilities
{
    public interface IDestructionEffect : IEffect
    {
        string Target { get; }
    }
}