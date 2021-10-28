using RLEngine.Controllers;
using RLEngine.Input;
using RLEngine.Events;
using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.Turns;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

using System.Collections.Generic;

namespace RLEngine.Games
{
    public class Game
    {
        private readonly PlayerController playerController;
        private readonly AIController aiController;
        private readonly EventTriggerer eventTriggerer;
        private readonly EventStack eventStack;
        private readonly ActionExecutor actionExecutor;
        private readonly TurnManager turnManager = new();

        public Game(GameContent content)
        {
            Content = content;

            Board = new Board(content.BoardSize, Content.FloorType);
            actionExecutor = new(turnManager, Board);
            eventStack = new(actionExecutor, turnManager, Board);
            eventTriggerer = new(eventStack);
            var eventContext = new EventContext(eventStack, actionExecutor, turnManager, Board);
            aiController = new(eventContext);
            playerController = new(eventContext);
        }

        public IBoard Board { get; }
        public GameContent Content { get; }
        public IEntity? CurrentAgent => turnManager.Current;
        public bool ExpectsInput => eventStack.Count == 0 && (CurrentAgent?.IsPlayer ?? false);

        public IPlayerInput? Input
        {
            set { playerController.Input = value; }
        }

        public IEnumerable<ILog> SetupExample()
        {
            var exc = actionExecutor;

            yield return exc.Modify(Content.WallType, new Coords(4, 4))!;
            yield return exc.Modify(Content.WallType, new Coords(5, 4))!;
            yield return exc.Modify(Content.WallType, new Coords(4, 5))!;
            yield return exc.Modify(Content.WallType, new Coords(5, 5))!;

            yield return exc.Spawn(Content.PlayerType, new Coords(1, 0), out var player)!;
            if (player != null) player.IsPlayer = true;

            yield return actionExecutor.Spawn(Content.GoblinType, new Coords(3, 0))!;
            yield return actionExecutor.Spawn(Content.GoblinType, new Coords(5, 0))!;
            yield return actionExecutor.Spawn(Content.GoblinType, new Coords(3, 2))!;
            yield return actionExecutor.Spawn(Content.GoblinType, new Coords(5, 2))!;
        }

        public ILog? ProcessStep()
        {
            var log = TryProcessStep();
            if (log == null) return null;
            eventTriggerer.Handle(log);
            return log;
        }

        private ILog? TryProcessStep()
        {
            bool unsuccessfulTurn;
            do
            {
                while (eventStack.Count > 0)
                {
                    var eventLog = eventStack.InvokeNext();
                    if (eventLog != null) return eventLog;
                }
                unsuccessfulTurn = !TryProcessTurn(out var turnLog);
                Input = null;
                if (turnLog != null) return turnLog;
            } while (!unsuccessfulTurn);
            return null;
        }

        private bool TryProcessTurn(out ILog? log)
        {
            log = null;
            if (turnManager.Current == null) return false;

            var current = turnManager.Current;
            var controller = (Controller)(current.IsPlayer ? playerController : aiController);
            if (!controller.TryProcessTurn(current, out log)) return false;

            turnManager.Next(100);

            return true;
        }
    }
}