namespace RLEngine.Entities
{
    public interface IEntityAttributes
    {
        string Name { get; }
        bool IsAgent { get; }
        int MaxHealth { get; }
        int Speed { get; }
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        bool IsGhost { get; }
        bool IsRoamer { get; }
    }
}