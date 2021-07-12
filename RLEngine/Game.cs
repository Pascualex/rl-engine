using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine
{
    public class Game : IGame
    {
        private readonly GameState state;
        private readonly IGameContent content;

        public Game(IGameContent content)
        {
            state = new(content.BoardSize, content.FloorType);
            this.content = content;
        }

        public IBoard Board => state.Board;

        public CombinedLog SetupExample()
        {
            var log = new CombinedLog();

            var spawnLog = state.Spawn(content.PlayerType, new Coords(1, 1));
            if (spawnLog == null) return log;

            var entity = spawnLog.Entity;
            log.Add(state.Move(entity, new Coords(1, 0), true));
            log.Add(state.Destroy(entity));

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