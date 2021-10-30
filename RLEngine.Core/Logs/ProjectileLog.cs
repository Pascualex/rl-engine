using RLEngine.Core.Utils;

namespace RLEngine.Core.Logs
{
    public class ProjectileLog : ILog
    {
        public ProjectileLog(ITarget source, ITarget target)
        {
            Source = source;
            Target = target;
        }

        public ITarget Source { get; }
        public ITarget Target { get; }
    }
}