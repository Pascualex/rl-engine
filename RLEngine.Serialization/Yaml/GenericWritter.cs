using RLEngine.Serialization.Yaml.Utils;

using RLEngine.Abilities;
using RLEngine.Actions;
using RLEngine.Utils;

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Yaml
{
    public class GenericWritter
    {
        private readonly SerializationQueue<IIdentifiable> serializationQueue;
        private readonly IEffectWritter effectWritter;
        private readonly Type[] inlineTypes = new[]
        {
            typeof(ActionAmount),
            typeof(Coords),
            typeof(Size),
        };

        public GenericWritter(SerializationQueue<IIdentifiable> serializationQueue)
        {
            this.serializationQueue = serializationQueue;
            effectWritter = new IEffectWritter(this);
        }

        public void WriteObject(TextWriter textWriter, object value, Type type)
        {
            var emitter = new Emitter(textWriter);
            emitter.Emit(new StreamStart());
            emitter.Emit(new DocumentStart());
            Write(emitter, value, type, true);
            emitter.Emit(new DocumentEnd(true));
            emitter.Emit(new StreamEnd());
        }

        public void WriteField(IEmitter emitter, object value, Type type)
        {
            Write(emitter, value, type, false);
        }

        private void Write(IEmitter emitter, object value, Type type, bool root)
        {
            if (!root && typeof(IIdentifiable).IsAssignableFrom(type))
            {
                var identifiable = (IIdentifiable)value;
                serializationQueue.Enqueue(identifiable, type);
                emitter.Format(identifiable.ID, ParseStyle.ID);
            }
            else if (type.IsPrimitive || type.IsEnum)
            {
                emitter.Format(value.ToString());
            }
            else if (type == typeof(string))
            {
                emitter.Format(value.ToString(), ParseStyle.String);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var enumerable = (IEnumerable)value;
                emitter.Emit(new SequenceStart(null, null, true, SequenceStyle.Block));
                var elementType = type.GetGenericArguments().Single();
                foreach (var element in enumerable)
                {
                    WriteField(emitter, element, elementType);
                }
                emitter.Emit(new SequenceEnd());
            }
            else if (typeof(IEffect).IsAssignableFrom(type))
            {
                effectWritter.WriteField(emitter, (IEffect)value);
            }
            else
            {
                var style = inlineTypes.Contains(type) ? MappingStyle.Flow : MappingStyle.Block;
                emitter.Emit(new MappingStart(null, null, true, style));
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