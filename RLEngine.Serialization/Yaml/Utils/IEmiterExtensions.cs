using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace RLEngine.Serialization.Yaml.Utils
{
    public static class IEmiterExtensions
    {
        public static void Format(this IEmitter emitter,
        string value, ParseStyle style = ParseStyle.Standard)
        {
            if (value.Length == 0)
            {
                emitter.Emit(new Scalar(string.Empty));
                return;
            }

            if (style == ParseStyle.String)
            {
                emitter.Emit(new Scalar(null, null, value, ScalarStyle.DoubleQuoted, true, false));
            }
            else if (style == ParseStyle.ID)
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
