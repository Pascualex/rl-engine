using RLEngine.Actions;

namespace RLEngine.Abilities
{
    public interface IAreaTargetEffect
    {
        string Source { get; }
        int Radius { get; }
        string NewGroup { get; }
    }
}