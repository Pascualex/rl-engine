using RLEngine.Controllers;
using RLEngine.Input;
using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

using System.Collections.Generic;

namespace RLEngine
{
    public class Game
    {
        private readonly PlayerController playerController = new();
        private readonly AIController aiController = new();
        private readonly GameState state;
        private readonly IGameContent content;

        public Game(IGameContent content)
        {
            state = new(content.BoardSize, content.FloorType);
            this.content = content;
        }

        public PlayerInput Input
        {
            set { playerController.Input = value; }
        }

        public CombinedLog SetupExample()
        {
            var log = new CombinedLog();

            log.Add(state.Spawn(content.PlayerType, new Coords(1, 0), out var player));
            if (player is not null) player.IsPlayer = true;
            log.Add(state.Spawn(content.GoblinType, new Coords(3, 0)));

            return log;
        }

        public CombinedLog ProcessTurns()
        {
            var log = new CombinedLog();
            var processed = new HashSet<Entity>();

            while (state.TurnManager.Current != null)
            {
                var current = state.TurnManager.Current;
                if (processed.Contains(current)) break;

                IController controller = current.IsPlayer ? playerController : aiController;
                if (!controller.ProcessTurn(current, state, out var turnLog)) break;
                log.Add(turnLog);

                processed.Add(current);
                state.TurnManager.Next(100);
            }

            return log;
        }
    }
}