using RLEngine.Boards;

namespace RLEngine.Content.Boards
{
    public class TileType : ITileType
    {
        public string Name { get; set; } = "NO_NAME";
        public bool BlocksGround { get; set; } = false;
        public bool BlocksAir { get; set; } = false;
        public object? Visuals { get; set; } = null;
    }
}