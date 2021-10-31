using RLEngine.Core.Logs;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public ProjectileLog? Shoot(ITarget source, ITarget target)
        {
            if (source is IEntity eSource && !eSource.IsActive) return null;
            if (target is IEntity eTarget && !eTarget.IsActive) return null;
            return new ProjectileLog(source, target);
        }
    }
}