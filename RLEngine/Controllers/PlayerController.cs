using RLEngine.Input;
using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using System;

namespace RLEngine.Controllers
{
    public class PlayerController : IController
    {
        public PlayerInput? Input { get; set; } = null;

        public bool ProcessTurn(Entity entity, GameState state, out Log? log)
        {
            log = null;
            if (Input is null) return false;

            if (Input is MoveInput mi) log = AttemptMove(entity, state, mi);

            Input = null;
            return log != null;
        }

        public Log? AttemptMove(Entity entity, GameState state, MoveInput moveInput)
        {
            var to = moveInput.To;
            if (!state.Board.TryGetCoords(entity, out var position)) return null;
            if (moveInput.Relative) to += position;

            foreach (var otherEntity in state.Board.GetEntities(to))
            {
                if (!otherEntity.IsPlayer)
                {
                    return state.Damage(otherEntity, entity, new ActionAmount { Base = 50 });
                }
            }

            return state.Move(entity, to, false);
        }
    }
}