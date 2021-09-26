using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace RLEngine.Content.Utils
{
    public static class IParserExtensions
    {
        public static string Consume(this IParser parser)
        {
            var value = parser.Consume<Scalar>().Value;

            if (value.Length == 0) return string.Empty;

            return char.ToUpperInvariant(value[0]) + value.Substring(1);
        }
    }
}
