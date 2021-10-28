using RLEngine.Entities;

namespace RLEngine.Logs
{
    public class DestructionLog : ILog
    {
        public DestructionLog(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; }
    }
}