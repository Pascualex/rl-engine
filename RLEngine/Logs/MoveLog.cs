using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class MoveLog : Log
    {
        public MoveLog(IROEntity entity, Coords to)
        {
            Entity = entity;
            To = to;
        }

        public IROEntity Entity { get; }
        public Coords To { get; }
    }
}