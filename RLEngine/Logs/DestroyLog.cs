using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class DestroyLog : Log
    {
        public DestroyLog(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; }
    }
}