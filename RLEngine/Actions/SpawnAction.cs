using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    internal static class SpawnAction
    {
        public static Log? Spawn(this GameState state,
        EntityType entityType, Coords at, out Entity? entity)
        {
            entity = new Entity(entityType);
            if (!state.Board.Add(entity, at))
            {
                entity = null;
                return null;
            }
            if (entity.IsAgent) state.TurnManager.Add(entity);
            return new SpawnLog(entity, at);
        }

        public static Log? Spawn(this GameState state,
        EntityType entityType, Coords at)
        {
            return Spawn(state, entityType, at, out _);
        }
    }
}