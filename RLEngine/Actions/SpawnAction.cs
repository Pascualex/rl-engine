using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    public static class SpawnAction
    {
        public static SpawnLog? Spawn(this GameState state,
        IEntityType entityType, Coords at)
        {
            var entity = new Entity(entityType);
            if (!state.Board.Add(entity, at)) return null;
            return new SpawnLog(entity, at);
        }
    }
}