using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class MovementLog : ILog
    {
        public MovementLog(IEntity entity, Coords to)
        {
            Entity = entity;
            To = to;
        }

        public IEntity Entity { get; }
        public Coords To { get; }
    }
}