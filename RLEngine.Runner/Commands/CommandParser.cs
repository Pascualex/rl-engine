using RLEngine.Utils;

using CommandLine;

namespace RLEngine.Runner
{
    public static class CommandParser
    {
        public static readonly Parser parser = new(config => config.HelpWriter = null);

        public static ParserResult<T> ParseArguments<T>(string[] args)
        {
            return parser.ParseArguments<T>(args);
        }

        public static Coords? ParseDirection(string direction)
        {
            if (AliasesUtils.Accepts(direction, new[] {    "up", "u" })) return Coords.   Up;
            if (AliasesUtils.Accepts(direction, new[] { "right", "r" })) return Coords.Right;
            if (AliasesUtils.Accepts(direction, new[] {  "down", "d" })) return Coords. Down;
            if (AliasesUtils.Accepts(direction, new[] {  "left", "l" })) return Coords. Left;
            return null;
        }
    }
}