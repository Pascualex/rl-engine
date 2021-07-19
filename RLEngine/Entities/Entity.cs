namespace RLEngine.Entities
{
    public class Entity : IEntityAttributes
    {
        public Entity(IEntityType type)
        {
            // TODO: support inheritance in types and overridden attributes
            Name = type.Name;
            IsAgent = type.IsAgent;
            Speed = type.Speed;
            BlocksGround = type.BlocksGround;
            BlocksAir = type.BlocksAir;
            IsGhost = type.IsGhost;
            IsRoamer = type.IsRoamer;
            Visuals = type.Visuals;
            Type = type;
        }

        public string Name { get; }
        public bool IsAgent { get; }
        public bool IsPlayer { get; set; } = false;
        public int Speed { get; }
        public bool BlocksGround { get; }
        public bool BlocksAir { get; }
        public bool IsGhost { get; }
        public bool IsRoamer { get; }
        public object? Visuals { get; }
        public IEntityType Type { get; }
    }
}