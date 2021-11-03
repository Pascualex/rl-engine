using RLEngine.Core.Utils;

using System;

namespace RLEngine.Core.Boards
{
    public class CoordsOutOfRangeException : Exception
    {
        public CoordsOutOfRangeException() : base()
        { }

        public CoordsOutOfRangeException(string message) : base(message)
        { }

        public CoordsOutOfRangeException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public CoordsOutOfRangeException(Coords coords, Size size)
        : base($"The coords {coords} are out of range for size {size}")
        { }
    }
}