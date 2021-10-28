using RLEngine.Logs;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Actions
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