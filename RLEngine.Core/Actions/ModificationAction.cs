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
            if (!board.CanModify(tileType, at)) return null;
            board.Modify(tileType, at);
            return new(tileType, previousType, at);
        }
    }
}