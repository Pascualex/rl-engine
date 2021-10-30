using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Logs
{
    public class SpawnLog : ILog
    {
        public SpawnLog(IEntity entity, Coords at)
        {
            Entity = entity;
            At = at;
        }

        public IEntity Entity { get; }
        public Coords At { get; }
    }
}