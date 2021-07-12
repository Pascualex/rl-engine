using RLEngine.Entities;
using RLEngine.Utils;

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