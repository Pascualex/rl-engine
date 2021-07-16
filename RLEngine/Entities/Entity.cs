namespace RLEngine.Entities
{
    public class Entity : IROEntity
    {
        public Entity(IEntityType type)
        {
            Type = type;
        }

        public string Name => Type.Name;
        public bool IsAgent => Type.IsAgent;
        public int Speed => Type.Speed;
        public bool BlocksGround => Type.BlocksGround;
        public bool BlocksAir => Type.BlocksAir;
        public bool IsGhost => Type.IsGhost;
        public object? Visuals => Type.Visuals;
        public IEntityType Type { get; }
    }
}