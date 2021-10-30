using RLEngine.Core.Logs;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public SpawnLog? Spawn(EntityType entityType, Coords at)
        {
            return Spawn(entityType, at, out _);
        }

        public SpawnLog? Spawn(EntityType entityType, Coords at, out IEntity? entity)
        {
            entity = new Entity(entityType);
            if (!board.Add(entity, at))
            {
                entity = null;
                return null;
            }
            if (entity.IsAgent) turnManager.Add(entity);
            return new(entity, at);
        }
    }
}