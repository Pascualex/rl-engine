using RLEngine.Logs;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
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