using RLEngine.Utils;

namespace RLEngine.Logs
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