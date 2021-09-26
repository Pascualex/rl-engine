﻿using RLEngine.Serialization.Abilities;
using RLEngine.Serialization.Utils;

using RLEngine.Abilities;

using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.TypeConverters
{
    public class IAbilityTypeConverter : IYamlTypeConverter
    {
        private readonly IEffectTypeConverter effectTC;

        public IAbilityTypeConverter(IEffectTypeConverter effectTC)
        {
            this.effectTC = effectTC;
        }

        public bool Accepts(Type type) => typeof(IAbility).IsAssignableFrom(type);

        public object ReadYaml(IParser parser, Type type)
        {
            var ability = new Ability();

            parser.Consume<MappingStart>();
            while (!parser.TryConsume<MappingEnd>(out _))
            {
                var name = parser.Consume();
                if (name == nameof(ability.Effect))
                {
                    var value = effectTC.ReadYaml(parser, typeof(Effect));
                    ability.SerializedEffect = (Effect)value;
                }
                else
                {
                    var value = parser.Consume();
                    ReflectionUtils.Assign(ability, name, value);
                }
            }

            return ability;
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            var ability = (IAbility)value!;

            emitter.Emit(new MappingStart());

            emitter.Emit(nameof(ability.Type));
            emitter.Emit(ability.Type.ToString());

            emitter.Emit(nameof(ability.Cost));
            emitter.Emit(ability.Cost.ToString());

            emitter.Emit(nameof(ability.Effect));
            effectTC.WriteYaml(emitter, ability.Effect, typeof(IEffect));

            emitter.Emit(new MappingEnd());
        }
    }
}