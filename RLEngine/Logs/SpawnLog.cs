using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class SpawnLog : Log
    {
        public SpawnLog(Entity entity, Coords at)
        {
            Entity = entity;
            At = at;
        }

        public Entity Entity { get; }
        public Coords At { get; }
    }
}