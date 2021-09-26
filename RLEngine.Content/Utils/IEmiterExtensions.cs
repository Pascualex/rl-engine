using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace RLEngine.Content.Utils
{
    public static class IEmiterExtensions
    {
        public static void Emit(this IEmitter emitter, string value)
        {
            if (value.Length == 0)
            {
                emitter.Emit(new Scalar(string.Empty));
                return;
            }

            var formattedValue = char.ToLowerInvariant(value[0]) + value.Substring(1);
            emitter.Emit(new Scalar(formattedValue));
        }
    }
}
