using RLEngine.Input;
using RLEngine.Events;
using RLEngine.Logs;
using RLEngine.Entities;

namespace RLEngine.Controllers
{
    internal class PlayerController : IController
    {
        public IPlayerInput? Input { private get; set; } = null;

        public bool TryProcessTurn(Entity entity, EventContext ctx, out Log? log)
        {
            log = null;
            if (Input == null) return false;

            if (Input is MovementInput mi) log = AttemptMove(entity, ctx, mi);
            else if (Input is AttackInput ati) log = AttempAttack(entity, ctx, ati);
            else if (Input is AbilityInput abi) log = AttemptCast(entity, ctx, abi);

            Input = null;
            return log != null;
        }

        public Log? AttemptMove(Entity entity, EventContext ctx, MovementInput input)
        {
            return ctx.Move(entity, input.To, input.Relative);
        }

        public Log? AttempAttack(Entity entity, EventContext ctx, AttackInput input)
        {
            return ctx.Damage(input.Target, entity, new ActionAmount { Base = 10 });
        }

        public Log? AttemptCast(Entity entity, EventContext ctx, AbilityInput input)
        {
            return ctx.Cast(entity, input.Target, input.Ability);
        }
    }
}