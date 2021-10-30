using RLEngine.Core.Logs;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Actions
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