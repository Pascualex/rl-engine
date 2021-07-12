namespace RLEngine.Entities
{
    public class Entity
    {
        public Entity(IEntityType type)
        {
            Type = type;
        }

        public IEntityType Type { get; }
    }
}