using RLEngine.Entities;

namespace RLEngine.Serialization.Entities
{
    public class EntityType : IEntityType
    {
        public string Name { get; set; } = "NO_NAME";
        public bool IsAgent { get; set; } = false;
        public bool BlocksGround { get; set; } = true;
        public bool BlocksAir { get; set; } = false;
        public bool IsGhost { get; set; } = false;
        public object? Visuals { get; set; } = null;
        public IEntityType? Parent { get; set; } = null;
    }
}