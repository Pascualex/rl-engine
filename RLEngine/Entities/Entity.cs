using RLEngine.Utils;

namespace  RLEngine.Entities
{
    public class Entity
    {
        public IEntityType Type { get; }

        public Entity(IEntityType type)
        {
            Type = type;
        }
    }
}