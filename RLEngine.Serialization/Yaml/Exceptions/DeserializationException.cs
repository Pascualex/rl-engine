using System;

namespace RLEngine.Serialization.Yaml
{
    public class DeserializationException : Exception
    {
        public DeserializationException() : base()
        { }

        public DeserializationException(string message) : base(message)
        { }

        public DeserializationException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public DeserializationException(Type type, string propertyName)
        : base($"The serialized property {propertyName} is not present in type {type}")
        { }
    }
}