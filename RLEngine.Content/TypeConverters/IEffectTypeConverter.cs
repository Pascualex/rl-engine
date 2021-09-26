using RLEngine.Content.Abilities;
using RLEngine.Content.Utils;

using RLEngine.Abilities;
using RLEngine.Actions;

using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Content.TypeConverters
{
    public class IEffectTypeConverter : IYamlTypeConverter
    {
        private readonly ActionAmountTypeConverter actionAmountTypeConverter = new();

        public bool Accepts(Type type) => typeof(IEffect).IsAssignableFrom(type);

        public object ReadYaml(IParser parser, Type type)
        {
            var effect = new Effect();

            parser.Consume<MappingStart>();
            while (!parser.TryConsume<MappingEnd>(out _))
            {
                var name = parser.Consume();
                if (name == nameof(effect.Effects))
                {
                    effect.SerializedEffects = new List<Effect>();
                    parser.Consume<SequenceStart>();
                    while (!parser.TryConsume<SequenceEnd>(out _))
                    {
                        var value = ReadYaml(parser, typeof(Effect));
                        effect.SerializedEffects.Add((Effect)value);
                    }
                }
                else if (name == nameof(effect.Amount))
                {
                    var value = actionAmountTypeConverter.ReadYaml(parser, typeof(ActionAmount));
                    effect.Amount = (ActionAmount)value;
                }
                else
                {
                    var value = parser.Consume();
                    ReflectionUtils.Assign(effect, name, value);
                }
            }

            return effect;
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            var effect = (IEffect)value!;

            emitter.Emit(new MappingStart());

            emitter.Emit(nameof(effect.Type));
            emitter.Emit(effect.Type.ToString());

            if (effect.Type == EffectType.Combined)
            {
                emitter.Emit(nameof(effect.IsParallel));
                emitter.Emit(effect.IsParallel.ToString());

                emitter.Emit(nameof(effect.Effects));
                emitter.Emit(new SequenceStart(null, null, false, SequenceStyle.Block));
                foreach (var nestedEffect in effect.Effects)
                {
                    WriteYaml(emitter, nestedEffect, typeof(IEffect));
                }
                emitter.Emit(new SequenceEnd());
            }
            else if (effect.Type == EffectType.Damage)
            {
                emitter.Emit(nameof(effect.Amount));
                actionAmountTypeConverter.WriteYaml(emitter, effect.Amount, typeof(ActionAmount));
            }

            emitter.Emit(new MappingEnd());
        }
    }
}