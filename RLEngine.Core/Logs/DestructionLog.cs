using RLEngine.Core.Entities;

namespace RLEngine.Core.Logs
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