using RLEngine.Input;
using RLEngine.Events;
using RLEngine.Logs;
using RLEngine.Entities;

namespace RLEngine.Controllers
{
    internal class PlayerController : Controller
    {
        public PlayerController(EventContext ctx)
        : base(ctx) { }

        public IPlayerInput? Input { private get; set; } = null;

        public override bool TryProcessTurn(IEntity entity, out ILog? log)
        {
            log = null;
            if (Input == null) return false;

            if (Input is MovementInput mi) log = AttemptMove(entity, ctx, mi);
            else if (Input is AttackInput ati) log = AttempAttack(entity, ctx, ati);
            else if (Input is AbilityInput abi) log = AttemptCast(entity, ctx, abi);

            Input = null;
            return log != null;
        }

        public ILog? AttemptMove(IEntity entity, EventContext ctx, MovementInput input)
        {
            return ctx.ActionExecutor.Move(entity, input.To, input.Relative);
        }

        public ILog? AttempAttack(IEntity entity, EventContext ctx, AttackInput input)
        {
            return ctx.ActionExecutor.Damage(input.Target, entity, new Amount { Base = 10 });
        }

        public ILog? AttemptCast(IEntity entity, EventContext ctx, AbilityInput input)
        {
            return ctx.ActionExecutor.Cast(entity, input.Target, input.Ability);
        }
    }
}