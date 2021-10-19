﻿using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;

namespace RLEngine.Actions
{
    internal static class DestructionAction
    {
        public static Log? Destroy(this GameState state,
        Entity entity)
        {
            if (!state.Board.Remove(entity)) return null;
            state.TurnManager.Remove(entity);
            entity.OnDestroy();
            return new DestructionLog(entity);
        }
    }
}