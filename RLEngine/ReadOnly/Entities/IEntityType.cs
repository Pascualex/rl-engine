namespace RLEngine.Entities
{
    public interface IEntityType
    {
        bool IsAgent { get; }
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        bool IsGhost { get; }
        object? Visuals { get; }
    }
}