using RLEngine.Serialization.Yaml.Utils;

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
    public class CustomTCSerializer : IYamlTypeConverter
    {
        private readonly SerializationQueue serializationQueue;
        private readonly IEffectTCSerializer effectCustomTCSerializer;
        private readonly Type[] inlineTypes = new[]
        {
            typeof(ActionAmount),
            typeof(Coords),
            typeof(Size),
        };

        public CustomTCSerializer(SerializationQueue serializationQueue)
        {
            this.serializationQueue = serializationQueue;
            effectCustomTCSerializer = new IEffectTCSerializer(this);
        }

        public bool Accepts(Type type) => true;

        public object ReadYaml(IParser parser, Type type)
        {
            throw new NotImplementedException();
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            if (value is null) throw new ArgumentNullException();
            WriteField(emitter, value, type, true);
        }

        public void WriteField(IEmitter emitter, object value, Type type)
        {
            WriteField(emitter, value, type, false);
        }

        private void WriteField(IEmitter emitter, object value, Type type, bool root)
        {
            if (effectCustomTCSerializer.Accepts(type))
            {
                effectCustomTCSerializer.WriteYaml(emitter, value, type);
                return;
            }

            if (type.IsPrimitive || type.IsEnum)
            {
                emitter.Format(value.ToString());
            }
            else if (type == typeof(string))
            {
                emitter.Format(value.ToString(), EmitterStyle.String);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var enumerable = (IEnumerable)value;
                emitter.Emit(new SequenceStart(null, null, false, SequenceStyle.Block));
                var elementType = type.GetGenericArguments().Single();
                foreach (var element in enumerable)
                {
                    WriteField(emitter, element, elementType);
                }
                emitter.Emit(new SequenceEnd());
            }
            else if (!root && typeof(IIdentifiable).IsAssignableFrom(type))
            {
                var identifiable = (IIdentifiable)value;
                serializationQueue.Enqueue(identifiable, type);
                emitter.Format(identifiable.ID, EmitterStyle.ID);
            }
            else
            {
                var style = inlineTypes.Contains(type) ? MappingStyle.Flow : MappingStyle.Block;
                emitter.Emit(new MappingStart(null, null, false, style));
                var isIdentifiable = typeof(IIdentifiable).IsAssignableFrom(type);
                foreach (var propertyInfo in type.GetPublicProperties())
                {
                    if (isIdentifiable && propertyInfo.Name == nameof(IIdentifiable.ID)) continue;
                    var ignore = propertyInfo.GetCustomAttribute(typeof(YamlIgnoreAttribute));
                    if (ignore is not null) continue;
                    var propertyValue = (object?)propertyInfo.GetValue(value);
                    if (propertyValue is null) continue;
                    var member = propertyInfo.GetCustomAttribute(typeof(YamlMemberAttribute))
                        as YamlMemberAttribute;
                    emitter.Format(member?.Alias ?? propertyInfo.Name);
                    WriteField(emitter, propertyValue, propertyInfo.PropertyType);
                }
                emitter.Emit(new MappingEnd());
            }
        }
    }
}