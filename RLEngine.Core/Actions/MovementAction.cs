using RLEngine.Core.Logs;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public MovementLog? Move(IEntity entity, Coords to, bool relative)
        {
            if (relative) to += entity.Position;
            if (!board.Move(entity, to)) return null;
            return new(entity, to);
        }
    }
}