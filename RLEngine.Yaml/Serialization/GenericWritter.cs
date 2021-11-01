using RLEngine.Yaml.Utils;

using RLEngine.Core.Abilities;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using System;
using System.Collections;
using System.IO;
using System.Linq;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace RLEngine.Yaml.Serialization
{
    internal class GenericWritter
    {
        private readonly SerializationQueue serializationQueue;
        private readonly EffectWritter effectWritter;
        private readonly Type[] inlineTypes = new[]
        {
            typeof(IAmount),
            typeof(Coords),
            typeof(Size),
        };

        public GenericWritter(SerializationQueue serializationQueue)
        {
            this.serializationQueue = serializationQueue;
            effectWritter = new EffectWritter(this);
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
            if (type.IsPrimitive || type.IsEnum)
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
            else if (typeof(Effect).IsAssignableFrom(type))
            {
                effectWritter.WriteField(emitter, (Effect)value);
            }
            else if (!root && typeof(IIdentifiable).IsAssignableFrom(type))
            {
                var identifiable = (IIdentifiable)value;
                serializationQueue.Enqueue(identifiable, type);
                emitter.Format(identifiable.ID, ParseStyle.ID);
            }
            else
            {
                var style = inlineTypes.Contains(type) ? MappingStyle.Flow : MappingStyle.Block;
                emitter.Emit(new MappingStart(null, null, true, style));
                var isIdentifiable = typeof(IIdentifiable).IsAssignableFrom(type);
                foreach (var propertyInfo in type.GetPublicProperties())
                {
                    if (isIdentifiable && propertyInfo.Name == nameof(IIdentifiable.ID)) continue;
                    var propertyValue = (object?)propertyInfo.GetValue(value);
                    if (propertyValue == null) continue;
                    emitter.Format(propertyInfo.Name);
                    WriteField(emitter, propertyValue, propertyInfo.PropertyType);
                }
                emitter.Emit(new MappingEnd());
            }
        }
    }
}