using RLEngine.Serialization.Yaml.Utils;

using RLEngine.Abilities;

using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Yaml
{
    public class EffectWritter
    {
        private readonly GenericWritter genericWritter;
        private readonly Dictionary<EffectType, string[]> effectTypesFields = new()
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
                    nameof(IEffect.Target),
                    nameof(IEffect.Source),
                }
            },
        };
        private readonly Dictionary<string, object> ignoreFilter = new()
        {
            { nameof(IEffect.Source), string.Empty },
        };

        public EffectWritter(GenericWritter CustomTCSerializer)
        {
            genericWritter = CustomTCSerializer;
        }

        public void WriteField(IEmitter emitter, IEffect effect)
        {
            emitter.Emit(new MappingStart(null, null, false, MappingStyle.Block));

            emitter.Format(nameof(effect.Type));
            genericWritter.WriteField(emitter, effect.Type, typeof(EffectType));

            foreach (var propertyName in effectTypesFields[effect.Type])
            {
                var propertyInfo = typeof(IEffect).GetProperty(propertyName);
                var propertyValue = (object?)propertyInfo.GetValue(effect);
                if (propertyValue == null) continue;
                var hasIgnoreFilter = ignoreFilter.TryGetValue(propertyName, out var ignoreValue);
                if (hasIgnoreFilter && propertyValue == ignoreValue) continue;
                emitter.Format(propertyName);
                genericWritter.WriteField(emitter, propertyValue, propertyInfo.PropertyType);
            }

            emitter.Emit(new MappingEnd());
        }
    }
}