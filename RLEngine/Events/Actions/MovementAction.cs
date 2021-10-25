using RLEngine.Logs;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Events
{
    internal static class MovementAction
    {
        public static MovementLog? Move(this EventContext ctx,
        Entity entity, Coords to, bool relative)
        {
            if (relative) to += entity.Position;
            if (!ctx.Board.Move(entity, to)) return null;
            return new MovementLog(entity, to);
        }
    }
}