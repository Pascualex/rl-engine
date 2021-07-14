using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class DestroyLog : Log
    {
        public DestroyLog(IROEntity entity)
        {
            Entity = entity;
        }

        public IROEntity Entity { get; }
    }
}