using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    public static class MoveAction
    {
        public static MoveLog? Move(this GameState state,
        Entity entity, Coords to, bool relative)
        {
            if (!state.Board.Move(entity, to, relative)) return null;
            to = (Coords)state.Board.GetCoords(entity)!;
            return new MoveLog(entity, to);
        }
    }
}