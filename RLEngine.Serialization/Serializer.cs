using RLEngine.Entities;

using System.IO;
using System.Text.Json;

namespace RLEngine.Serialization
{
    public static class Serializer
    {
        public static void Main(string[] args)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            var entityType = new EntityType() { Name = "Gamusino" };

            var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(entityType, options);
            File.WriteAllBytes("prueba.json", utf8Bytes);
        }
    }
}