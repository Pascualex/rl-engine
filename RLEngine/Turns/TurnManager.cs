using RLEngine.Entities;

using System;
using System.Collections.Generic;

namespace RLEngine.Turns
{
    public class TurnManager
    {
        private readonly SortedSet<Turn> turns = new();
        private readonly Dictionary<Entity, Turn> entities = new();
        private int nextEntityId = 0;

        public Entity? Current => turns.Count > 0 ? turns.Min.Entity : null;

        public bool Add(Entity entity)
        {
            if (entities.ContainsKey(entity)) return false;

            var currentTick = turns.Count > 0 ? turns.Min.Tick : 0;
            var turn = new Turn(currentTick, nextEntityId, entity);
            nextEntityId++;
            turns.Add(turn);
            entities.Add(entity, turn);

            return true;
        }

        public bool Remove(Entity entity)
        {
            if (!entities.TryGetValue(entity, out var turn)) return false;

            turns.Remove(turn);
            entities.Remove(entity);

            return true;
        }

        public void NextTurn(int actionCost)
        {
            if (actionCost <= 0) return;
            if (turns.Count == 0) return;

            var turn = turns.Min;
            var realActionCost = (int)Math.Round(actionCost * (100f / turn.Entity.Speed));
            var nextTurn = turn.NewAfterTicks(realActionCost);

            turns.Remove(turn);
            turns.Add(nextTurn);
            entities[nextTurn.Entity] = nextTurn;
        }
    }
}