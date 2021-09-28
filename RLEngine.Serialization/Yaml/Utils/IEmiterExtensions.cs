using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace RLEngine.Serialization.Yaml.Utils
{
    public static class IEmiterExtensions
    {
        public static void Format(this IEmitter emitter, string value)
        {
            Format(emitter, value, EmitterStyle.Standard);
        }

        public static void Format(this IEmitter emitter, string value, EmitterStyle style)
        {
            if (value.Length == 0)
            {
                emitter.Emit(new Scalar(string.Empty));
                return;
            }

            if (style == EmitterStyle.String)
            {
                emitter.Emit(new Scalar(null, null, value, ScalarStyle.DoubleQuoted, true, false));
            }
            else if (style == EmitterStyle.ID)
            {
                emitter.Emit(new Scalar("$" + value));
            }
            else
            {
                emitter.Emit(new Scalar(char.ToLowerInvariant(value[0]) + value.Substring(1)));
            }
        }
    }
}
