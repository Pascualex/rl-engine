using RLEngine.Serialization.Utils;

using RLEngine.Actions;
using RLEngine.Utils;

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
    public class CustomTypeConverter : IYamlTypeConverter
    {
        private readonly IEffectTypeConverter effectTypeConverter;
        private readonly Type[] inlineTypes = new[]
        {
            typeof(ActionAmount),
        };

        public CustomTypeConverter()
        {
            effectTypeConverter = new IEffectTypeConverter(this);
        }

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
            if (effectTypeConverter.Accepts(type))
            {
                effectTypeConverter.WriteYaml(emitter, value, type);
                return;
            }

            if (value == null)
            {
                emitter.Format("Null");
            }
            else if (type.IsPrimitive || type.IsEnum || type == typeof(string))
            {
                emitter.Format(value.ToString());
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var enumerable = (IEnumerable)value;
                emitter.Emit(new SequenceStart(null, null, false, SequenceStyle.Block));
                var elementType = type.GetGenericArguments().Single();
                foreach (var element in enumerable)
                {
                    WriteYaml(emitter, element, elementType);
                }
                emitter.Emit(new SequenceEnd());
            }
            else
            {
                var style = inlineTypes.Contains(type) ? MappingStyle.Flow : MappingStyle.Block;
                emitter.Emit(new MappingStart(null, null, false, style));
                var isIdentifiable = typeof(IIdentifiable).IsAssignableFrom(type);
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (isIdentifiable && propertyInfo.Name == nameof(IIdentifiable.ID)) continue;
                    var ignore = propertyInfo.GetCustomAttribute(typeof(YamlIgnoreAttribute));
                    if (ignore is not null) continue;
                    var member = propertyInfo.GetCustomAttribute(typeof(YamlMemberAttribute))
                        as YamlMemberAttribute;
                    emitter.Format(member?.Alias ?? propertyInfo.Name);
                    var propertyValue = propertyInfo.GetValue(value);
                    WriteYaml(emitter, propertyValue, propertyInfo.PropertyType);
                }
                emitter.Emit(new MappingEnd());
            }
        }

        private PropertyInfo GetPropertyOrAlias(Type type, string propertyName)
        {
            var propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo is not null)
            {
                var ignore = propertyInfo.GetCustomAttribute(typeof(YamlIgnoreAttribute));
                if (ignore is null) return propertyInfo;
            }

            var aliasPropertiesInfo = type.GetProperties()
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