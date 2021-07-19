using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class DestroyLog : Log
    {
        public DestroyLog(Entity entity)
        {
            Entity = entity;
        }

        public Entity Entity { get; }
    }
}