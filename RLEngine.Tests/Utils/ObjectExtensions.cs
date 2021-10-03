using NUnit.Framework;

namespace RLEngine.Tests.Utils
{
    public static class ObjectExtensions
    {
        public static T FailIfNull<T>(this T? obj)
        {
            return obj ?? throw new AssertionException("Expected: not null\n  But was:  null");
        }
    }
}