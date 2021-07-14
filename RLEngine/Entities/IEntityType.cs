namespace RLEngine.Entities
{
    public interface IEntityType
    {
        string Name { get; }
        bool IsAgent { get; }
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        bool IsGhost { get; }
        object? Visuals { get; }
        IEntityType? Parent { get; }
    }
}