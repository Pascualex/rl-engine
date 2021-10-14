namespace RLEngine.Abilities
{
    public interface ISourceEffect : ITargetEffect
    {
        string Source { get; }
    }
}