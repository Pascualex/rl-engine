using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System;

namespace RLEngine.Core.Boards
{
    internal class SynchronizationException : Exception
    {
        public SynchronizationException() : base()
        { }

        public SynchronizationException(string message) : base(message)
        { }

        public SynchronizationException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public SynchronizationException(IEntity entity, Coords coords)
        : base($"The entity {entity} was not found at its supposed position {coords}")
        { }
    }
}