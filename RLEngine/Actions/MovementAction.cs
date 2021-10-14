using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    public static class MovementAction
    {
        public static Log? Move(this GameState state,
        Entity entity, Coords to, bool relative)
        {
            if (!state.Board.TryGetCoords(entity, out var position)) return null;
            if (relative) to += position;

            if (!state.Board.Move(entity, to)) return null;
            return new MovementLog(entity, to);
        }
    }
}