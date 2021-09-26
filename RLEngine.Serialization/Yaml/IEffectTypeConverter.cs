using RLEngine.Serialization.Utils;

using RLEngine.Abilities;

using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Yaml
{
    public class IEffectTypeConverter : IYamlTypeConverter
    {
        private readonly CustomTypeConverter customTypeConverter;
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

        public IEffectTypeConverter(CustomTypeConverter customTypeConverter)
        {
            this.customTypeConverter = customTypeConverter;
        }

        public bool Accepts(Type type) => typeof(IEffect).IsAssignableFrom(type);

        public object ReadYaml(IParser parser, Type type)
        {
            return customTypeConverter.ReadYaml(parser, type);
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            var effect = (IEffect)value!;

            emitter.Emit(new MappingStart(null, null, false, MappingStyle.Block));

            emitter.Format(nameof(effect.Type));
            customTypeConverter.WriteYaml(emitter, effect.Type, typeof(EffectType));

            foreach (var propertyName in filter[effect.Type])
            {
                emitter.Format(propertyName);
                var propertyInfo = typeof(IEffect).GetProperty(propertyName);
                var propertyValue = propertyInfo.GetValue(effect);
                customTypeConverter.WriteYaml(emitter, propertyValue, propertyInfo.PropertyType);
            }

            emitter.Emit(new MappingEnd());
        }
    }
}