using RLEngine.Controllers;
using RLEngine.Input;
using RLEngine.Events;
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
        private readonly PlayerController playerController = new();
        private readonly AIController aiController = new();
        private readonly EventQueue eventQueue = new();
        private readonly EventContext eventCtx;
        private readonly TurnManager turnManager = new();

        public Game(GameContent content)
        {
            Content = content;

            Board = new Board(content.BoardSize, Content.FloorType);
            eventCtx = new(eventQueue, turnManager, Board);
        }

        public Board Board { get; }
        public GameContent Content { get; }
        public Entity? CurrentAgent => turnManager.Current;
        public bool ExpectsInput => eventQueue.Count == 0 && (CurrentAgent?.IsPlayer ?? false);

        public IPlayerInput? Input
        {
            set { playerController.Input = value; }
        }

        public IEnumerable<Log> SetupExample()
        {
            Log? log = eventCtx.Modify(Content.WallType, new Coords(4, 4));
            if (log != null) yield return log;
            log = eventCtx.Modify(Content.WallType, new Coords(5, 4));
            if (log != null) yield return log;
            log = eventCtx.Modify(Content.WallType, new Coords(4, 5));
            if (log != null) yield return log;
            log = eventCtx.Modify(Content.WallType, new Coords(5, 5));
            if (log != null) yield return log;

            log = eventCtx.Spawn(Content.PlayerType, new Coords(1, 0), out var entity);
            if (entity != null) entity.IsPlayer = true;
            if (log != null) yield return log;

            log = eventCtx.Spawn(Content.GoblinType, new Coords(3, 0));
            if (log != null) yield return log;
            log = eventCtx.Spawn(Content.GoblinType, new Coords(5, 0));
            if (log != null) yield return log;
            log = eventCtx.Spawn(Content.GoblinType, new Coords(3, 2));
            if (log != null) yield return log;
            log = eventCtx.Spawn(Content.GoblinType, new Coords(5, 2));
            if (log != null) yield return log;
        }

        public Log? ProcessStep()
        {
            bool unsuccessfulTurn;
            do
            {
                while (eventQueue.Count > 0)
                {
                    var eventLog = eventQueue.Invoke();
                    if (eventLog != null) return eventLog;
                }
                unsuccessfulTurn = !TryProcessTurn(out var turnLog);
                Input = null;
                if (turnLog != null) return turnLog;
            } while (!unsuccessfulTurn);
            return null;
        }

        private bool TryProcessTurn(out Log? log)
        {
            log = null;
            if (turnManager.Current == null) return false;

            var current = turnManager.Current;
            var controller = (IController)(current.IsPlayer ? playerController : aiController);
            if (!controller.TryProcessTurn(current, eventCtx, out log)) return false;

            turnManager.Next(100);

            return true;
        }
    }
}