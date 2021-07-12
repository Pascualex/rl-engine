using System;

namespace RLEngine.Entities
{
    public class EntityType : IEntityType
    {
        public bool IsAgent { get; set; } = false;
        public bool BlocksGround { get; set; } = true;
        public bool BlocksAir { get; set; } = false;
        public bool IsGhost { get; set; } = false;
        public object? Visuals { get; set; } = null;
    }
}