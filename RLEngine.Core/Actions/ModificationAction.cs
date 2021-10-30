using RLEngine.Core.Logs;
using RLEngine.Core.Boards;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Actions
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