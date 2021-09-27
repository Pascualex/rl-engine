using RLEngine.Boards;

namespace RLEngine.Serialization.Boards
{
    public class TileType : ITileType
    {
        public string ID { get; set; } = "NO_ID";
        public string Name { get; set; } = "NO_NAME";
        public bool BlocksGround { get; set; } = false;
        public bool BlocksAir { get; set; } = false;
        public object? Visuals { get; set; } = null;
    }
}