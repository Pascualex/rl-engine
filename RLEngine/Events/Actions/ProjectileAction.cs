using RLEngine.Logs;

namespace RLEngine.Events
{
    internal static class ProjectileAction
    {
        public static ProjectileLog? Shoot(this EventContext ctx,
        ITarget source, ITarget target)
        {
            return new ProjectileLog(source, target);
        }
    }
}