using RLEngine.Logs;
using RLEngine.Abilities;
using RLEngine.Entities;
using RLEngine.Utils;

using System.Collections.Generic;
using System.Linq;

using NRE = System.NullReferenceException;

namespace RLEngine.Events
{
    internal class AreaTargetEffectEvent : EffectEvent<IAreaTargetEffect>
    {
        public AreaTargetEffectEvent(IAreaTargetEffect effect, TargetDB targetDB, EventContext ctx)
        : base(effect, targetDB, ctx)
        { }

        protected override Log? InternalInvoke()
        {
            var from = targetDB.GetCoords(effect.Source);
            if (from == null)
            {
                if (!targetDB.TryGetEntity(effect.Source, out var source)) throw new NRE();
                if (source.IsDestroyed)
                {
                    targetDB.Add(effect.NewGroup, Enumerable.Empty<Entity>());
                    return null;
                }
                from = source.Position;
            }

            var downLeft = from - new Coords(effect.Radius, effect.Radius);
            var  upRight = from + new Coords(effect.Radius, effect.Radius);

            var entities = new List<Entity>();

            for (var i = downLeft.Y; i <= upRight.Y; i++)
            {
                for (var j = downLeft.X; j <= upRight.X; j++)
                {
                    var current = new Coords(j, i);
                    var currentEntities = ctx.Board.GetEntities(current);
                    entities.AddRange(currentEntities);
                }
            }

            targetDB.Add(effect.NewGroup, entities);

            return null;
        }
    }
}
