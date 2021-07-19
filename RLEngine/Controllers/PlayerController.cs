using RLEngine.Input;
using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine.Controllers
{
    public class PlayerController : IController
    {
        public PlayerInput? Input { get; set; } = null;

        public bool ProcessTurn(Entity entity, GameState state, out Log? log)
        {
            log = null;
            if (Input is null) return false;

            if (Input is MoveInput mi) log = state.Move(entity, mi.To, mi.Relative);

            Input = null;
            return log != null;
        }
    }
}