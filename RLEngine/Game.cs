using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine
{
    public class Game
    {
        private readonly GameState state;
        private readonly IGameContent content;

        public Game(IGameContent content)
        {
            state = new(content.BoardSize, content.FloorType);
            this.content = content;
        }

        public CombinedLog SetupExample()
        {
            var log = new CombinedLog();

            log.Add(state.Spawn(content.PlayerType, new Coords(1, 1), out var player));
            if (player is null) return log;

            log.Add(state.Move(player, new Coords(1, 0), true));
            log.Add(state.Move(player, new Coords(1, 0), true));
            log.Add(state.Modify(content.WallType, new Coords(4, 1)));

            log.Add(state.Spawn(content.GoblinType, new Coords(3, 2), out var goblin));
            if (goblin is null) return log;

            log.Add(state.Damage(player, goblin, new ActionAmount() { Base = 8 }));
            log.Add(state.Heal(player, player, new ActionAmount() { Base = 5 }));
            log.Add(state.Heal(player, player, new ActionAmount() { Base = 5 }));
            log.Add(state.Damage(goblin, player, new ActionAmount() { Base = 6 }));
            log.Add(state.Damage(goblin, player, new ActionAmount() { Base = 6 }));

            return log;
        }

        public CombinedLog ProcessTurns()
        {
            var combinedLog = new CombinedLog();
            return combinedLog;
        //     var turnAction = new ParallelAction();
        //     var processed = new HashSet<Agent>();

        //     Agent? agent;
        //     while (((agent = TurnManager.GetCurrent()) != null) && !processed.Contains(agent))
        //     {
        //         if (agent.IsPlayer) LastPlayer = agent;
        //         Controller controller = agent.IsPlayer ? PlayerController : AIController;
        //         var agentAction = controller.ProduceAction(agent, Ctx);

        //         if (agentAction == null) break;

        //         agentAction.Execute();

        //         if (!agentAction.IsCompleted) turnAction.Add(agentAction);

        //         processed.Add(agent);
        //         TurnManager.Next();

        //         if (agentAction is not MoveAction and not NullAction) break;
        //     }

        //     return turnAction;
        }
    }
}