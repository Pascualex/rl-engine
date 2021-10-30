using RLEngine.Core.Utils;

namespace RLEngine.Core.Boards
{
    public class TileType : IIdentifiable
    {
        public string ID { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public bool BlocksGround { get; init; } = false;
        public bool BlocksAir { get; init; } = false;
    }
}