using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Logs
{
    public class ProjectileLog : Log
    {
        internal ProjectileLog(ITarget source, ITarget target)
        {
            Source = source;
            Target = target;
        }

        public ITarget Source { get; }
        public ITarget Target { get; }
    }
}