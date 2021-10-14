﻿using RLEngine.Actions;
using RLEngine.Logs;
using RLEngine.State;

using NRE = System.NullReferenceException;

namespace RLEngine.Abilities
{
    public static class DestructionEffect
    {
        public static Log? Destroy(this ITargetEffect effect,
        TargetDB targetDB, GameState state)
        {
            if (!targetDB.TryGetEntity(effect.Target, out var target)) throw new NRE();

            return state.Destroy(target);
        }
    }
}