using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace RLEngine.Serialization.Yaml.Utils
{
    public static class IParserExtensions
    {
        public static string Formatted(this IParser parser, ParseStyle style = ParseStyle.Standard)
        {
            var value = parser.Consume<Scalar>().Value;
            if (value.Length == 0) return string.Empty;
            if (style == ParseStyle.ID) return value.Substring(1);
            else return char.ToUpperInvariant(value[0]) + value.Substring(1);
        }

        public static bool TryFormatted(this IParser parser,
        out string value, ParseStyle style = ParseStyle.Standard)
        {
            value = string.Empty;
            if (!parser.TryConsume<Scalar>(out var scalar)) return false;
            value = scalar.Value;
            if (value.Length == 0) return false;
            if (style == ParseStyle.ID) value = value.Substring(1);
            else value = char.ToUpperInvariant(value[0]) + value.Substring(1);
            return true;
        }
    }
}
