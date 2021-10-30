using RLEngine.Yaml.Utils;

using RLEngine.Core.Abilities;
using RLEngine.Core.Utils;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;

using EFE = RLEngine.Yaml.Serialization.EffectSerializationException;

namespace RLEngine.Yaml.Serialization
{
    public class EffectWritter
    {
        private readonly GenericWritter genericWritter;

        public EffectWritter(GenericWritter CustomTCSerializer)
        {
            genericWritter = CustomTCSerializer;
        }

        public void WriteField(IEmitter emitter, Effect effect)
        {
            emitter.Emit(new MappingStart(null, null, false, MappingStyle.Block));

            emitter.Format(nameof(effect.Type));
            genericWritter.WriteField(emitter, effect.Type, typeof(EffectType));

            var effectType = effect.GetEffectType();
            if (effectType == null) throw new EFE(effect.Type);

            foreach (var propertyInfo in effectType.GetPublicProperties())
            {
                if (propertyInfo.Name == nameof(IIdentifiable.ID)) continue;
                var propertyValue = (object?)propertyInfo.GetValue(effect);
                if (propertyValue == null) continue;
                if (propertyValue is string str && str.Length == 0) continue;
                emitter.Format(propertyInfo.Name);
                genericWritter.WriteField(emitter, propertyValue, propertyInfo.PropertyType);
            }

            emitter.Emit(new MappingEnd());
        }
    }
}