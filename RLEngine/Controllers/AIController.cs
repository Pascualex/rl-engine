using RLEngine.Events;
using RLEngine.Logs;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Controllers
{
    internal class AIController : IController
    {
        public bool TryProcessTurn(Entity entity, EventContext ctx, out Log? log)
        {
            log = RandomMovement(entity, ctx);
            return true;
        }

        private Log? RandomMovement(Entity entity, EventContext ctx)
        {
            if (!entity.IsRoamer) return null;

            var directions = Coords.RandomizedDirections();
            var selectedDirection = Coords.Zero;
            foreach (var direction in directions)
            {
                var to = entity.Position + direction;
                if (!ctx.Board.CanMove(entity, to))
                {
                    selectedDirection = direction;
                    break;
                }
            }

            if (selectedDirection == Coords.Zero) return null;

            return ctx.Move(entity, selectedDirection, true);
        }
    }
}