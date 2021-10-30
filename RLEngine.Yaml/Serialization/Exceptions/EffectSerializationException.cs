using RLEngine.Core.Abilities;

using System;

namespace RLEngine.Yaml.Serialization
{
    public class EffectSerializationException : SerializationException
    {
        public EffectSerializationException() : base()
        { }

        public EffectSerializationException(string message) : base(message)
        { }

        public EffectSerializationException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public EffectSerializationException(EffectType effectType)
        : base($"The effect type {effectType} does not have an assigned type")
        { }
    }
}