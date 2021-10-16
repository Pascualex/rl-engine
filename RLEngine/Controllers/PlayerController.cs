using RLEngine.Abilities;
using RLEngine.Input;
using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

using System.Linq;
using System.Collections.Generic;

namespace RLEngine.Controllers
{
    public class PlayerController : IController
    {
        public PlayerInput? Input { private get; set; } = null;

        public bool ProcessTurn(Entity entity, GameState state, out Log? log)
        {
            log = null;
            if (Input is null) return false;

            if (Input is MovementInput mi) log = AttemptMove(entity, state, mi);
            else if (Input is AttackInput ai) log = AttempAttack(entity, state, ai);
            else if (Input is SpellInput si) log = AttemptCast(entity, state, si);

            Input = null;
            return log != null;
        }

        public Log? AttemptMove(Entity entity, GameState state, MovementInput movementInput)
        {
            var to = movementInput.To;
            if (!state.Board.TryGetCoords(entity, out var position)) return null;
            if (movementInput.Relative) to += position;
            return state.Move(entity, to, false);
        }

        public Log? AttempAttack(Entity entity, GameState state, AttackInput attackInput)
        {
            if (!state.Board.TryGetCoords(entity, out var position)) return null;
            var targetPosition = position + attackInput.Coords;
            var entities = state.Board.GetEntities(targetPosition);
            if (!entities.Any()) return null;
            return state.Damage(entities.First(), entity, new ActionAmount { Base = 10 });
        }

        public Log? AttemptCast(Entity entity, GameState state, SpellInput spellInput)
        {
            if (!state.Board.TryGetCoords(entity, out var position)) return null;
            var targetPosition = position + spellInput.Coords;
            var entities = state.Board.GetEntities(targetPosition);
            if (!entities.Any()) return null;
            return spellInput.Ability.Cast(entity, entities.First(), state);
        }
    }
}