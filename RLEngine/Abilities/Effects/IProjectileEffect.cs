namespace RLEngine.Abilities
{
    public interface IProjectileEffect : IEffect
    {
        string Target { get; }
        string Source { get; }
    }
}