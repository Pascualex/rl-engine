using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class EntityTarget : ITarget
    {
        public Entity Entity { get; }

        public EntityTarget(Entity entity)
        {
            Entity = entity;
        }
    }
}