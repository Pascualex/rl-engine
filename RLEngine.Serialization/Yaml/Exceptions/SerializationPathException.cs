using System;

namespace RLEngine.Serialization.Yaml
{
    public class SerializationPathException : Exception
    {
        public SerializationPathException() : base()
        { }

        public SerializationPathException(string message) : base(message)
        { }

        public SerializationPathException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public SerializationPathException(Type type)
        : base($"The type {type} doesn't have a serialization path defined")
        { }
    }
}