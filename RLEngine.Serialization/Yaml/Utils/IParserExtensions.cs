using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace RLEngine.Serialization.Yaml.Utils
{
    public static class IParserExtensions
    {
        public static string Formatted(this IParser parser)
        {
            var value = parser.Consume<Scalar>().Value;
            if (value.Length == 0) return string.Empty;
            return char.ToUpperInvariant(value[0]) + value.Substring(1);
        }

        public static bool TryFormatted(this IParser parser, out string value)
        {
            value = string.Empty;
            if (!parser.TryConsume<Scalar>(out var scalar)) return false;
            value = scalar.Value;
            if (value.Length == 0) return false;
            value = char.ToUpperInvariant(value[0]) + value.Substring(1);
            return true;
        }
    }
}
