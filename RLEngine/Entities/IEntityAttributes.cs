namespace RLEngine.Entities
{
    public interface IEntityAttributes
    {
        string Name { get; }
        bool IsAgent { get; }
        int Speed { get; }
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        bool IsGhost { get; }
        bool IsRoamer { get; }
        object? Visuals { get; }
    }
}