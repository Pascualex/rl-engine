using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RLEngine
{
    public static class Serializer
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            Converters = {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            }
        };

        public static void Serialize<T>(T obj, string filename)
        {
            var jsonString = JsonSerializer.Serialize(obj, Options);
            File.WriteAllText(filename, jsonString);
        }

        public static T? Deserialize<T>(string filename)
        {
            var jsonString = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<T>(jsonString, Options);
        }
    }
}