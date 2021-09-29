using RLEngine.Serialization.Yaml.Utils;

using RLEngine.Abilities;

using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Yaml
{
    public class IEffectWritter
    {
        private readonly GenericWritter genericWritter;
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

        public IEffectWritter(GenericWritter CustomTCSerializer)
        {
            genericWritter = CustomTCSerializer;
        }

        public void WriteField(IEmitter emitter, IEffect effect)
        {
            emitter.Emit(new MappingStart(null, null, false, MappingStyle.Block));

            emitter.Format(nameof(effect.Type));
            genericWritter.WriteField(emitter, effect.Type, typeof(EffectType));

            foreach (var propertyName in filter[effect.Type])
            {
                var propertyInfo = typeof(IEffect).GetProperty(propertyName);
                var propertyValue = (object?)propertyInfo.GetValue(effect);
                if (propertyValue is null) continue;
                emitter.Format(propertyName);
                genericWritter.WriteField(emitter, propertyValue, propertyInfo.PropertyType);
            }

            emitter.Emit(new MappingEnd());
        }
    }
}