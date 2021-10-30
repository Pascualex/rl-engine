using RLEngine.Core.Logs;
using RLEngine.Core.Abilities;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System;
using System.Collections.Generic;

using NRE = System.NullReferenceException;

namespace RLEngine.Core.Events
{
    internal class AreaTargetEffectEvent : EffectEvent<IAreaTargetEffect>
    {
        public AreaTargetEffectEvent(IAreaTargetEffect effect, TargetDB targetDB)
        : base(effect, targetDB)
        { }

        protected override ILog? InternalInvoke(EventContext ctx)
        {
            var from = targetDB.GetCoords(effect.Source);
            if (from == null)
            {
                if (!targetDB.TryGetEntity(effect.Source, out var source)) throw new NRE();
                if (source.IsDestroyed)
                {
                    targetDB.Add(effect.NewGroup, Array.Empty<Entity>());
                    return null;
                }
                from = source.Position;
            }

            var downLeft = from - new Coords(effect.Radius, effect.Radius);
            var  upRight = from + new Coords(effect.Radius, effect.Radius);

            var entities = new List<IEntity>();

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
