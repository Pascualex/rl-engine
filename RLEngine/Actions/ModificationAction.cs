using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Boards;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    public static class ModificationAction
    {
        public static Log? Modify(this GameState state,
        TileType tileType, Coords at)
        {
            var previousType = state.Board.GetTileType(at);
            if (previousType == null) return null;
            if (!state.Board.Modify(tileType, at)) return null;
            return new ModificationLog(tileType, previousType, at);
        }
    }
}