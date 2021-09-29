using RLEngine.Serialization.Utils;

using RLEngine.Entities;

using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Entities
{
    public class EntityType : Deserializable, IEntityType
    {
        public string Name { get; set; } = "NO_NAME";
        public bool IsAgent { get; set; } = false;
        public int MaxHealth { get; set; } = 100;
        public int Speed { get; set; } = 100;
        public bool BlocksGround { get; set; } = true;
        public bool BlocksAir { get; set; } = false;
        public bool IsGhost { get; set; } = false;
        public bool IsRoamer { get; set; } = true;
        public object? Visuals { get; set; } = null;
        [YamlMember(SerializeAs = typeof(EntityType))]
        public IEntityType? Parent { get; set; } = null;
    }
}