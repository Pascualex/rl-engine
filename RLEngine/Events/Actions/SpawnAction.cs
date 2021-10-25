using RLEngine.Logs;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Events
{
    internal static class SpawnAction
    {
        public static SpawnLog? Spawn(this EventContext ctx,
        EntityType entityType, Coords at)
        {
            return Spawn(ctx, entityType, at, out _);
        }

        public static SpawnLog? Spawn(this EventContext ctx,
        EntityType entityType, Coords at, out Entity? entity)
        {
            entity = new Entity(entityType);
            if (!ctx.Board.Add(entity, at))
            {
                entity = null;
                return null;
            }
            if (entity.IsAgent) ctx.TurnManager.Add(entity);
            return new SpawnLog(entity, at);
        }
    }
}