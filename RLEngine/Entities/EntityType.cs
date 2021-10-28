using RLEngine.Utils;

namespace RLEngine.Entities
{
    public class EntityType : IIdentifiable, IEntityAttributes
    {
        public string ID { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public bool IsAgent { get; init; } = false;
        public int MaxHealth { get; init; } = 100;
        public int Speed { get; init; } = 100;
        public bool BlocksGround { get; init; } = true;
        public bool BlocksAir { get; init; } = false;
        public bool IsGhost { get; init; } = false;
        public bool IsRoamer { get; init; } = true;
        public EntityType? Parent { get; init; } = null;
    }
}