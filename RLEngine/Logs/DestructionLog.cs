using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class DestructionLog : Log
    {
        internal DestructionLog(Entity entity)
        {
            Entity = entity;
        }

        public Entity Entity { get; }
    }
}