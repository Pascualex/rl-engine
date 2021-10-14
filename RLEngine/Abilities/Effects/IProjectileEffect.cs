using RLEngine.Utils;

namespace RLEngine.Abilities
{
    public interface IProjectileEffect : ISourceEffect
    {
        string From { get; }
        string To { get; }
    }
}