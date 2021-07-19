using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class MoveLog : Log
    {
        public MoveLog(Entity entity, Coords to)
        {
            Entity = entity;
            To = to;
        }

        public Entity Entity { get; }
        public Coords To { get; }
    }
}