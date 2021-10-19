using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Entities;
using RLEngine.Utils;

using System.Collections.Generic;

using NRE = System.NullReferenceException;

namespace RLEngine.Abilities
{
    internal static class AreaTargetEffect
    {
        public static Log? TargetArea(this IAreaTargetEffect effect,
        TargetDB targetDB, GameState state)
        {
            var from = targetDB.GetCoords(effect.Source);
            if (from == null)
            {
                if (!targetDB.TryGetEntity(effect.Source, out var source)) throw new NRE();
                if (!state.Board.TryGetCoords(source, out from)) throw new NRE();
            }

            var downLeft = from - new Coords(effect.Radius, effect.Radius);
            var  upRight = from + new Coords(effect.Radius, effect.Radius);

            var entities = new List<Entity>();

            for (var i = downLeft.Y; i <= upRight.Y; i++)
            {
                for (var j = downLeft.X; j <= upRight.X; j++)
                {
                    var current = new Coords(j, i);
                    var currentEntities = state.Board.GetEntities(current);
                    entities.AddRange(currentEntities);
                }
            }

            targetDB.Add(effect.NewGroup, entities);

            return null;
        }
    }
}
