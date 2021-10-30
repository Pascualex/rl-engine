using RLEngine.Core.Logs;
using RLEngine.Core.Entities;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public DestructionLog? Destroy(IEntity entity)
        {
            if (!board.Remove(entity)) return null;
            turnManager.Remove(entity);
            entity.OnDestroy();
            return new(entity);
        }
    }
}