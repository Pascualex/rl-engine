using RLEngine.Core.Logs;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public SpawnLog? Spawn(EntityType entityType, Coords at)
        {
            var entity = new Entity(entityType);
            if (!board.Add(entity, at)) return null;
            if (entity.IsAgent) turnManager.Add(entity);
            return new(entity, at);
        }
    }
}