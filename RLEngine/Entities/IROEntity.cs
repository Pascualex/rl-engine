namespace RLEngine.Entities
{
    public interface IROEntity
    {
        string Name { get; }
        bool IsAgent { get; }
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        bool IsGhost { get; }
        IEntityType Type { get; }
    }
}