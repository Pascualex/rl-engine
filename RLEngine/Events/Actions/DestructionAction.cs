using RLEngine.Logs;
using RLEngine.Entities;

namespace RLEngine.Events
{
    internal static class DestructionAction
    {
        public static DestructionLog? Destroy(this EventContext ctx,
        Entity entity)
        {
            if (!ctx.Board.Remove(entity)) return null;
            ctx.TurnManager.Remove(entity);
            entity.OnDestroy();
            return new DestructionLog(entity);
        }
    }
}