using RLEngine.Serialization.Yaml.Utils;

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Yaml
{
    public class CustomTCDeserializer : IYamlTypeConverter
    {
        public bool Accepts(Type type) => true;

        public object ReadYaml(IParser parser, Type type)
        {
            if (parser.TryFormatted(out var valueStr))
            {
                var typeDescriptor = TypeDescriptor.GetConverter(type);
                return typeDescriptor.ConvertFromInvariantString(valueStr);
            }
            else if (parser.TryConsume<SequenceStart>(out _))
            {
                var value = (IList)Activator.CreateInstance(type);
                var elementType = type.GetGenericArguments().Single();
                while (!parser.TryConsume<SequenceEnd>(out _))
                {
                    var itemValue = ReadYaml(parser, elementType);
                    value.Add(itemValue);
                }
                return value;
            }
            else if (parser.TryConsume<MappingStart>(out _))
            {
                var value = Activator.CreateInstance(type);
                while (!parser.TryConsume<MappingEnd>(out _))
                {
                    var propertyName = parser.Formatted();
                    var propertyInfo = GetPropertyOrAlias(type, propertyName);
                    var propertyValue = ReadYaml(parser, propertyInfo.PropertyType);
                    propertyInfo.SetValue(value, propertyValue);
                }
                return value;
            }
            else
            {
                throw new FormatException();
            }
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            throw new NotImplementedException();
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
    }
}