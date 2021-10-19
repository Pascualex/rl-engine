using System;

namespace RLEngine.Runner
{
    public static class AliasesUtils
    {
        public static bool Accepts(string command, string[] aliases)
        {
            foreach (var alias in aliases)
            {
                if (string.Equals(command, alias, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }
    }
}