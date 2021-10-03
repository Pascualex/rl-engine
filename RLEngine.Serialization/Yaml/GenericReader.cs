using RLEngine.Serialization.Yaml.Utils;
using RLEngine.Serialization.Utils;

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Yaml
{
    public class GenericReader
    {
        private readonly SerializationQueue<Deserializable> serializationQueue;

        public GenericReader(SerializationQueue<Deserializable> serializationQueue)
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
            else if (!root && typeof(Deserializable).IsAssignableFrom(type))
            {
                var id = parser.Formatted(ParseStyle.ID);
                if (serializationQueue.TryGetValue(id, type, out var value)) return value;
                value = (Deserializable)(target ?? Activator.CreateInstance(type));
                value.ID = id;
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
                    var propertyInfo = GetPropertyOrAlias(type, propertyName);
                    var propertyType = GetSerializationType(propertyInfo);
                    var propertyTarget = propertyInfo.GetValue(value);
                    var propertyValue = ReadField(parser, propertyType, propertyTarget);
                    propertyInfo.SetValue(value, propertyValue);
                }
                return value;
            }
        }

        private PropertyInfo GetPropertyOrAlias(Type type, string propertyName)
        {
            var propertyInfo = type.GetPublicProperty(propertyName);
            if (propertyInfo is not null)
            {
                var ignore = propertyInfo.GetCustomAttribute(typeof(YamlIgnoreAttribute));
                if (ignore is null) return propertyInfo;
            }

            var aliasPropertiesInfo = type.GetPublicProperties()
                .Where(x => Attribute.IsDefined(x, typeof(YamlMemberAttribute)));

            foreach (var aliasPropertyInfo in aliasPropertiesInfo)
            {
                var attribute = aliasPropertyInfo.GetCustomAttribute(typeof(YamlMemberAttribute));
                var member = (YamlMemberAttribute)attribute;
                if (member.Alias == propertyName) return aliasPropertyInfo;
            }

            throw new FormatException();
        }

        private Type GetSerializationType(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute(typeof(YamlMemberAttribute));
            var member = (YamlMemberAttribute?)attribute;
            return member?.SerializeAs ?? propertyInfo.PropertyType;
        }
    }
}