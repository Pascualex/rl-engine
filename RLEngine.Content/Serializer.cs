using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RLEngine
{
    public static class Serializer
    {
        private static readonly ISerializer serializer = new SerializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults)
            .Build();

        private static readonly IDeserializer deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();

        public static void Serialize<T>(T obj, string filename)
        {
            if (obj is null) return;
            var yamlString = serializer.Serialize(obj);
            File.WriteAllText(filename, yamlString);
        }

        public static T? Deserialize<T>(string filename)
        {
            var jsonString = File.ReadAllText(filename);
            return deserializer.Deserialize<T>(jsonString);
        }
    }
}