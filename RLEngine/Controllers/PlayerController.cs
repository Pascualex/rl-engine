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

            if (Input is MoveInput mi) log = AttemptMove(entity, state, mi);
            else if (Input is AttackInput ati) log = AreaAttack(entity, state, ati);
            else if (Input is AbilityInput abi) log = CastSpell(entity, state, abi);

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
                    return state.Damage(otherEntity, entity, new ActionAmount { Base = 10 });
                }
            }

            return state.Move(entity, to, false);
        }

        public Log? AreaAttack(Entity entity, GameState state, AttackInput attackInput)
        {
            if (!state.Board.TryGetCoords(entity, out var position)) return null;

            var log = new CombinedLogBuilder(true);

            foreach (var direction in Coords.Directions())
            {
                var entities = new List<Entity>(state.Board.GetEntities(position + direction));
                foreach (var otherEntity in entities)
                {
                    log.Add(state.Damage(otherEntity, entity, new ActionAmount { Base = 10 }));
                }
            }

            return log.Build();
        }

        public Log? CastSpell(Entity entity, GameState state, AbilityInput abilityInput)
        {
            if (!state.Board.TryGetCoords(entity, out var position)) return null;
            var targetPosition = position + abilityInput.Coords;
            var entities = state.Board.GetEntities(targetPosition);
            if (!entities.Any()) return null;
            return abilityInput.Ability.Cast(entity, entities.First(), state);
        }
    }
}