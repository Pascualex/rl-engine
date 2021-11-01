using RLEngine.Core.Entities;

using System;
using System.Collections.Generic;

namespace RLEngine.Core.Turns
{
    internal class TurnManager : ITurnManager
    {
        private readonly SortedSet<Turn> turns = new();
        private readonly Dictionary<IEntity, Turn> entities = new();
        private int nextEntityId = 0;

        public IEntity? Current => turns.Count > 0 ? turns.Min.Entity : null;

        public void Add(IEntity entity)
        {
            if (!entity.IsAgent) return;
            if (entities.ContainsKey(entity)) return;

            var currentTick = turns.Count > 0 ? turns.Min.Tick : 0;
            var turn = new Turn(currentTick, nextEntityId, entity);
            nextEntityId++;
            turns.Add(turn);
            entities.Add(entity, turn);
        }

        public void Remove(IEntity entity)
        {
            if (!entities.TryGetValue(entity, out var turn)) return;

            turns.Remove(turn);
            entities.Remove(entity);
        }

        public void Next(int actionCost)
        {
            if (actionCost <= 0) return;
            if (turns.Count == 0) return;

            var turn = turns.Min;
            var speed = Math.Max(1, turn.Entity.Speed);
            var realActionCost = Math.Max(1, (int)Math.Round(actionCost * (100f / speed)));
            var nextTurn = turn.NewAfterTicks(realActionCost);

            turns.Remove(turn);
            turns.Add(nextTurn);
            entities[nextTurn.Entity] = nextTurn;

            var nextEntity = Current;
            if (nextEntity == null) return;
            if (!nextEntity.IsAgent) Remove(nextEntity);
        }
    }
}