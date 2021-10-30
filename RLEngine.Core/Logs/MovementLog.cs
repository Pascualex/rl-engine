using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Logs
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