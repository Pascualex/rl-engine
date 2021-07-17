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

            log.Add(state.Spawn(content.PlayerType, new Coords(1, 0)));
            log.Add(state.Spawn(content.GoblinType, new Coords(3, 0)));

            return log;
        }

        public CombinedLog ProcessTurns()
        {
            var log = new CombinedLog();
            var processed = new HashSet<Entity>();

            while (state.TurnManager.Current != null)
            {
                var agent = state.TurnManager.Current;
                if (processed.Contains(agent)) break;

                log.Add(state.Move(agent, new Coords(0, 1), true));

                processed.Add(agent);
                state.TurnManager.Next(100);
            }

            return log;
        }
    }
}