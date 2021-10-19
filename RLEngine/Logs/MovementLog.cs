using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class MovementLog : Log
    {
        internal MovementLog(Entity entity, Coords to)
        {
            Entity = entity;
            To = to;
        }

        public Entity Entity { get; }
        public Coords To { get; }
    }
}