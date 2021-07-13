namespace RLEngine.Boards
{
    public interface ITileType
    {
        string Name { get; }
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        object? Visuals { get; }
    }
}