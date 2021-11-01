using RLEngine.Core.Logs;
using RLEngine.Core.Entities;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public DestructionLog? Destroy(IEntity entity)
        {
            if (!entity.IsActive) return null;
            board.Remove(entity);
            turnManager.Remove(entity);
            return new(entity);
        }
    }
}