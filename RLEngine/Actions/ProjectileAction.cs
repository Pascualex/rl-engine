using RLEngine.Logs;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    internal partial class ActionExecutor
    {
        public ProjectileLog? Shoot(ITarget source, ITarget target)
        {
            if (source is IEntity eSource && eSource.IsDestroyed) return null;
            if (target is IEntity eTarget && eTarget.IsDestroyed) return null;
            return new ProjectileLog(source, target);
        }
    }
}