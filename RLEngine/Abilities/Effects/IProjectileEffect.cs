namespace RLEngine.Abilities
{
    public interface IProjectileEffect
    {
        string Target { get; }
        string Source { get; }
        string From { get; }
        string To { get; }
    }
}