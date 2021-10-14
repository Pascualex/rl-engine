using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class DestructionLog : Log
    {
        public DestructionLog(Entity entity)
        {
            Entity = entity;
        }

        public Entity Entity { get; }
    }
}