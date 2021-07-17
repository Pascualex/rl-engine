using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    public static class SpawnAction
    {
        public static Log? Spawn(this GameState state,
        IEntityType entityType, Coords at, out Entity? entity)
        {
            entity = new Entity(entityType);
            if (!state.Board.Add(entity, at))
            {
                entity = null;
                return null;
            }
            state.TurnManager.Add(entity);
            return new SpawnLog(entity, at);
        }

        public static Log? Spawn(this GameState state,
        IEntityType entityType, Coords at)
        {
            return Spawn(state, entityType, at, out _);
        }
    }
}