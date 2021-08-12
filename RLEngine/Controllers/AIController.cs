using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Controllers
{
    public class AIController : IController
    {
        public bool ProcessTurn(Entity entity, GameState state, out Log? log)
        {
            if (RandomMovement(entity, state, out log)) return true;
            log = null;
            return true;
        }

        private bool RandomMovement(Entity entity, GameState state, out Log? log)
        {
            log = null;
            if (!entity.IsRoamer) return false;

            if (state.Board.TryGetCoords(entity, out var position)) return false;

            var directions = Coords.RandomizedDirections();
            var selectedDirection = Coords.Zero;
            foreach (var direction in directions)
            {
                var to = position + direction;
                if (!state.Board.CanMove(entity, to))
                {
                    selectedDirection = direction;
                    break;
                }
            }

            if (selectedDirection == Coords.Zero) return false;

            log = state.Move(entity, selectedDirection, true);
            return true;
        }
    }
}