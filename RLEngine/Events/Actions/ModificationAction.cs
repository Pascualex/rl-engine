using RLEngine.Logs;
using RLEngine.Boards;
using RLEngine.Utils;

namespace RLEngine.Events
{
    internal static class ModificationAction
    {
        public static ModificationLog? Modify(this EventContext ctx,
        TileType tileType, Coords at)
        {
            var previousType = ctx.Board.GetTileType(at);
            if (previousType == null) return null;
            if (!ctx.Board.Modify(tileType, at)) return null;
            return new ModificationLog(tileType, previousType, at);
        }
    }
}