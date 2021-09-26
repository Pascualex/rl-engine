using RLEngine.Content.Utils;

using RLEngine.Actions;

using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace RLEngine.Content.TypeConverters
{
    public class ActionAmountTypeConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => typeof(ActionAmount).IsAssignableFrom(type);

        public object ReadYaml(IParser parser, Type type)
        {
            var amount = new ActionAmount();

            parser.Consume<MappingStart>();
            while (!parser.TryConsume<MappingEnd>(out _))
            {
                var name = parser.Consume();
                var value = parser.Consume();
                ReflectionUtils.Assign(amount, name, value);
            }

            return amount;
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            var amount = (ActionAmount)value!;

            emitter.Emit(new MappingStart());

            if (amount.Base != 0)
            {
                emitter.Emit(nameof(amount.Base));
                emitter.Emit(amount.Base.ToString());
            }

            emitter.Emit(new MappingEnd());
        }
    }
}