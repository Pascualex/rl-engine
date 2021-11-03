using RLEngine.Core.Entities;

using System;

namespace RLEngine.Core.Boards
{
    public class EntityInactiveException : Exception
    {
        public EntityInactiveException() : base()
        { }

        public EntityInactiveException(string message) : base(message)
        { }

        public EntityInactiveException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public EntityInactiveException(IEntity entity)
        : base($"The entity {entity} is inactive and not present in the board")
        { }
    }
}