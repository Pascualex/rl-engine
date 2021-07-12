using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

namespace RLEngine
{
    public class Game : IGame
    {
        public IBoard Board => board;
        // public readonly PlayerController PlayerController;
        // public readonly AIController AIController;
        // public readonly Pathfinder Pathfinder;
        // public readonly DungeonGenerator DungeonGenerator;
        // public readonly TurnManager TurnManager;
        // public readonly Action.Context Ctx;
        // public Agent? LastPlayer { get; private set; }

        // private Action MainAction = new NullAction();
        private readonly Board board;
        private readonly IGameContent content;

        public Game(IGameContent content)
        {
            // var defaultMovement = gameSO.DefaultMovement;
            // var testAbilitySO = gameSO.TestAbilitySO.Value();
            // PlayerController = new PlayerController(defaultMovement, testAbilitySO);
            // AIController = new AIController(defaultMovement);
            // Pathfinder = new Pathfinder();
            // DungeonGenerator = new DungeonGenerator();
            // TurnManager = new TurnManager();
            board = new(content.BoardSize, content.FloorType);
            // var agentPrefab = gameSO.AgentPrefab.Value();
            // var entityPrefab = gameSO.EntityPrefab.Value();
            // Ctx = new Action.Context(TurnManager, Board, agentPrefab, entityPrefab);
            this.content = content;

            SetupExample1();
        }

        private void SetupExample1()
        {
        //     var newAction = new ParallelAction();

        //     var playerAction = new SequentialAction();
        //     var playerAgentSO = gameSO.PlayerAgentSO.Value();
        //     var spawnPlayerAction = new SpawnAction(playerAgentSO, new Tile(3, 3), Ctx, true);
        //     playerAction.Add(spawnPlayerAction);
        //     playerAction.Add(new ControlAction(spawnPlayerAction.Agent, true));
        //     newAction.Add(playerAction);

        //     for (var i = 0; i < gameSO.EnemiesSOs.Length; i++)
        //     {
        //         var enemySO = gameSO.EnemiesSOs[i];
        //         if (enemySO == null) continue;
        //         newAction.Add(new SpawnAction(enemySO, new Tile(3 + i, 7), Ctx));
        //     }

        //     var boxEntitySO = gameSO.BoxEntitySO.Value();
        //     newAction.Add(new CreateAction(boxEntitySO, new Tile(3, 9), Ctx));

        //     MainAction = newAction;
        // }

        // private void SetupExample2(GameSO gameSO)
        // {
        //     var newAction = new ParallelAction();

        //     var dungeonSO = gameSO.DungeonSO.Value();
        //     var dungeonData = DungeonGenerator.GenerateDungeon(dungeonSO);
        //     // TODO: DungeonVisualizer.DungeonData = dungeonData;
        //     if (dungeonData == null) throw new NullReferenceException();

        //     Board.Apply(dungeonData.ToBoardData());

        //     var playerAction = new SequentialAction();
        //     var playerAgentSO = gameSO.PlayerAgentSO.Value();
        //     var playerSpawn = dungeonData.PlayerSpawn;
        //     var spawnPlayerAction = new SpawnAction(playerAgentSO, playerSpawn, Ctx, true);
        //     playerAction.Add(spawnPlayerAction);
        //     playerAction.Add(new ControlAction(spawnPlayerAction.Agent, true));
        //     newAction.Add(playerAction);

        //     if (gameSO.EnemiesSOs.Length > 0)
        //     {
        //         foreach (var spawn in dungeonData.EnemiesSpawn)
        //         {
        //             var randomIndex = Random.Range(0, gameSO.EnemiesSOs.Length);
        //             var enemySO = gameSO.EnemiesSOs[randomIndex];
        //             if (enemySO == null) continue;
        //             newAction.Add(new SpawnAction(enemySO, spawn, Ctx));
        //         }
        //     }

        //     MainAction = newAction;
        }

        public void GameLoop()
        {
            // if (MainAction.IsCompleted) MainAction = ProcessTurns();
            // else MainAction.Execute();
        }

        // private Action ProcessTurns()
        // {
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
        // }
    }
}