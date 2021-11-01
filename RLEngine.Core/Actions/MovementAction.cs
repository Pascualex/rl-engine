using RLEngine.Core.Logs;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Actions
{
    internal partial class ActionExecutor
    {
        public MovementLog? Move(IEntity entity, Coords to)
        {
            if (!entity.IsActive) return null;
            if (!board.CanMove(entity, to)) return null;
            board.Move(entity, to);
            return new(entity, to);
        }
    }
}