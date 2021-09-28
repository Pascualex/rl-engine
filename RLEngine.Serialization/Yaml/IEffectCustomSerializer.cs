using RLEngine.Serialization.Utils;

using RLEngine.Abilities;

using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Yaml
{
    public class IEffectCustomSerializer : IYamlTypeConverter
    {
        private readonly CustomSerializer customSerializer;
        private readonly Dictionary<EffectType, string[]> filter = new()
        {
            {
                EffectType.Combined,
                new[]
                {
                    nameof(IEffect.IsParallel),
                    nameof(IEffect.Effects),
                }
            },
            {
                EffectType.Damage,
                new[]
                {
                    nameof(IEffect.Amount),
                }
            },
        };

        public IEffectCustomSerializer(CustomSerializer customSerializer)
        {
            this.customSerializer = customSerializer;
        }

        public bool Accepts(Type type) => typeof(IEffect).IsAssignableFrom(type);

        public object ReadYaml(IParser parser, Type type)
        {
            throw new NotImplementedException();
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            var effect = (IEffect)value!;

            emitter.Emit(new MappingStart(null, null, false, MappingStyle.Block));

            emitter.Format(nameof(effect.Type));
            customSerializer.WriteField(emitter, effect.Type, typeof(EffectType));

            foreach (var propertyName in filter[effect.Type])
            {
                var propertyInfo = typeof(IEffect).GetProperty(propertyName);
                var propertyValue = (object?)propertyInfo.GetValue(effect);
                if (propertyValue is null) continue;
                emitter.Format(propertyName);
                customSerializer.WriteField(emitter, propertyValue, propertyInfo.PropertyType);
            }

            emitter.Emit(new MappingEnd());
        }
    }
}