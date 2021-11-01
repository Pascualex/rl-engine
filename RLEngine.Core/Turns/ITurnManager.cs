using RLEngine.Core.Entities;

using System;
using System.Collections.Generic;

namespace RLEngine.Core.Turns
{
    internal interface ITurnManager
    {
        IEntity? Current { get; }

        void Add(IEntity entity);
        void Remove(IEntity entity);
        void Next(int actionCost);
    }
}