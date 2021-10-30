using RLEngine.Core.Entities;

namespace RLEngine.Core.Logs
{
    public class PlayerLog : ILog
    {
        public PlayerLog(IEntity entity, bool madePlayer)
        {
            Entity = entity;
            MadePlayer = madePlayer;
        }

        public IEntity Entity { get; }
        public bool MadePlayer { get; }
    }
}