using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    public static class DestroyAction
    {
        public static DestroyLog? Destroy(this GameState state,
        Entity entity)
        {
            if (!state.Board.Remove(entity)) return null;
            return new DestroyLog(entity);
        }
    }
}