using RLEngine.Core.Entities;

using System;

namespace RLEngine.Core.Boards
{
    internal class EntityActiveException : Exception
    {
        public EntityActiveException() : base()
        { }

        public EntityActiveException(string message) : base(message)
        { }

        public EntityActiveException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public EntityActiveException(IEntity entity)
        : base($"The entity {entity} is already active and present in the board")
        { }
    }
}