using Random = System.Random;

namespace RLEngine.Utils
{
    public static class RandomExtensions
    {
        public static void Shuffle<T>(this Random random, T[] array)
        {
            var n = array.Length;
            for (var i = n - 1; i >= 0; i--)
            {
                var k = random.Next(i + 1);
                var temp = array[i];
                array[i] = array[k];
                array[k] = temp;
            }
        }
    }
}