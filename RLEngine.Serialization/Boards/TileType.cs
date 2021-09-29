using RLEngine.Serialization.Utils;

using RLEngine.Boards;

using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Boards
{
    public class TileType : Deserializable, ITileType
    {
        public string Name { get; set; } = "NO_NAME";
        public bool BlocksGround { get; set; } = false;
        public bool BlocksAir { get; set; } = false;
        public object? Visuals { get; set; } = null;
    }
}