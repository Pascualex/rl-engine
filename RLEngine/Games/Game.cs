using RLEngine.Controllers;
using RLEngine.Input;
using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

using System.Collections.Generic;

namespace RLEngine.Games
{
    public class Game
    {
        private readonly PlayerController playerController = new();
        private readonly AIController aiController = new();
        private readonly GameState state;

        public Game(GameContent content)
        : this(new(content.BoardSize, content.FloorType), content) { }

        public Game(GameState state, GameContent content)
        {
            this.state = state;
            Content = content;
        }

        public GameContent Content { get; }

        public PlayerInput? Input
        {
            set { playerController.Input = value; }
        }

        public CombinedLog SetupExample()
        {
            var log = new CombinedLogBuilder(false);

            log.Add(state.Modify(Content.WallType, new Coords(4, 4)));
            log.Add(state.Modify(Content.WallType, new Coords(5, 4)));
            log.Add(state.Modify(Content.WallType, new Coords(4, 5)));
            log.Add(state.Modify(Content.WallType, new Coords(5, 5)));

            log.Add(state.Spawn(Content.PlayerType, new Coords(1, 0), out var player));
            if (player is null) return log.ForceBuild();
            player.IsPlayer = true;
            log.Add(state.Spawn(Content.GoblinType, new Coords(3, 0), out var goblinA));
            if (goblinA is null) return log.ForceBuild();
            log.Add(state.Spawn(Content.GoblinType, new Coords(5, 0), out var goblinB));
            if (goblinB is null) return log.ForceBuild();
            log.Add(state.Spawn(Content.GoblinType, new Coords(3, 2), out var goblinC));
            if (goblinC is null) return log.ForceBuild();
            log.Add(state.Spawn(Content.GoblinType, new Coords(5, 2), out var goblinD));
            if (goblinD is null) return log.ForceBuild();

            return log.ForceBuild();
        }

        public CombinedLog ProcessTurns()
        {
            var log = new CombinedLogBuilder(false);
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

            return log.ForceBuild();
        }
    }
}