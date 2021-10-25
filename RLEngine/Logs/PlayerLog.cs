using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class PlayerLog : Log
    {
        internal PlayerLog(Entity entity, bool madePlayer)
        {
            Entity = entity;
            MadePlayer = madePlayer;
        }

        public Entity Entity { get; }
        public bool MadePlayer { get; }
    }
}