using RLEngine.Utils;

namespace RLEngine.Boards
{
    public interface ITileType : IIdentifiable
    {
        string Name { get; }
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        object? Visuals { get; }
    }
}