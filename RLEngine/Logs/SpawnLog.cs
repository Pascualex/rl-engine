using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class SpawnLog : Log
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