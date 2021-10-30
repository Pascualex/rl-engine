using RLEngine.Core.Events;
using RLEngine.Core.Logs;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

namespace RLEngine.Core.Controllers
{
    internal class AIController : Controller
    {
        public AIController(EventContext ctx)
        : base(ctx) { }

        public override bool TryProcessTurn(IEntity entity, out ILog? log)
        {
            log = RandomMovement(entity);
            return true;
        }

        private ILog? RandomMovement(IEntity entity)
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

            return ctx.ActionExecutor.Move(entity, selectedDirection, true);
        }
    }
}