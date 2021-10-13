using RLEngine.Yaml.Utils;

using RLEngine.Utils;

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.ComponentModel;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

using DE = RLEngine.Yaml.Serialization.DeserializationException;

namespace RLEngine.Yaml.Serialization
{
    public class GenericReader
    {
        private readonly SerializationQueue serializationQueue;

        public GenericReader(SerializationQueue serializationQueue)
        {
            this.serializationQueue = serializationQueue;
        }

        public object ReadObject(TextReader textReader, Type type, object? target = null)
        {
            var parser = new Parser(textReader);
            parser.Consume<StreamStart>();
            parser.Consume<DocumentStart>();
            var value = Read(parser, type, target, true);
            parser.Consume<DocumentEnd>();
            parser.Consume<StreamEnd>();
            return value;
        }

        public object ReadField(IParser parser, Type type, object? target = null)
        {
            return Read(parser, type, target, false);
        }

        private object Read(IParser parser, Type type, object? target, bool root)
        {
            if (type.IsPrimitive || type.IsEnum)
            {
                var valueStr = parser.Formatted();
                var typeDescriptor = TypeDescriptor.GetConverter(type);
                return typeDescriptor.ConvertFromInvariantString(valueStr);
            }
            else if (type == typeof(string))
            {
                return parser.Formatted(ParseStyle.String);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                parser.Consume<SequenceStart>();
                var value = (IList)(target ?? Activator.CreateInstance(type));
                var elementType = type.GetGenericArguments().Single();
                while (!parser.TryConsume<SequenceEnd>(out _))
                {
                    var itemValue = ReadField(parser, elementType);
                    value.Add(itemValue);
                }
                return value;
            }
            else if (!root && typeof(IIdentifiable).IsAssignableFrom(type))
            {
                var id = parser.Formatted(ParseStyle.ID);
                if (serializationQueue.TryGetValue(id, type, out var value)) return value;
                value = (IIdentifiable)(target ?? Activator.CreateInstance(type));
                var idProperty = type.GetPublicProperty(nameof(IIdentifiable.ID));
                if (idProperty is null) throw new DE(type, nameof(IIdentifiable.ID));
                idProperty.SetValue(value, id);
                serializationQueue.Enqueue(value, type);
                return value;
            }
            else
            {
                parser.Consume<MappingStart>();
                var value = target ?? Activator.CreateInstance(type);
                while (!parser.TryConsume<MappingEnd>(out _))
                {
                    var propertyName = parser.Formatted();
                    var propertyInfo = type.GetPublicProperty(propertyName);
                    if (propertyInfo is null) throw new DE(type, propertyName);
                    var propertyType = propertyInfo.PropertyType;
                    var propertyTarget = propertyInfo.GetValue(value);
                    var propertyValue = ReadField(parser, propertyType, propertyTarget);
                    propertyInfo.SetValue(value, propertyValue);
                }
                return value;
            }
        }
    }
}