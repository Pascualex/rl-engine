using System;

namespace RLEngine.Serialization.Yaml
{
    public class UnsupportedTypeException : Exception
    {
        public UnsupportedTypeException() : base()
        { }

        public UnsupportedTypeException(string message) : base(message)
        { }

        public UnsupportedTypeException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public UnsupportedTypeException(Type type)
        : base($"The type {type} is unsupported")
        { }
    }
}