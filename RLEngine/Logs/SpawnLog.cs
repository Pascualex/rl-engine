using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class SpawnLog : Log
    {
        public SpawnLog(IROEntity entity, Coords at)
        {
            Entity = entity;
            At = at;
        }

        public IROEntity Entity { get; }
        public Coords At { get; }
    }
}