using RLEngine.Logs;
using RLEngine.Boards;
using RLEngine.Utils;

namespace RLEngine.Actions
{
    internal partial class ActionExecutor
    {
        public ModificationLog? Modify(TileType tileType, Coords at)
        {
            var previousType = board.GetTileType(at);
            if (previousType == null) return null;
            if (!board.Modify(tileType, at)) return null;
            return new(tileType, previousType, at);
        }
    }
}