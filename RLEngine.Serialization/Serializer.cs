using RLEngine.Serialization.TypeConverters;
using RLEngine.Serialization.Abilities;

using RLEngine.Abilities;

using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeResolvers;
using YamlDotNet.Serialization.NamingConventions;

namespace RLEngine
{
    public static class Serializer
    {
        private static readonly ISerializer serializer = new SerializerBuilder()
            .WithTypeConverter(new IAbilityTypeConverter())
            .Build();

        private static readonly IDeserializer deserializer = new DeserializerBuilder()
            .WithTypeConverter(new IAbilityTypeConverter())
            .Build();

        public static void Serialize<T>(T value, string filename)
        {
            if (value is null) return;
            var yamlString = serializer.Serialize(value);
            File.WriteAllText(filename, yamlString);
        }

        public static T? Deserialize<T>(string filename)
        {
            var jsonString = File.ReadAllText(filename);
            return deserializer.Deserialize<T>(jsonString);
        }
    }
}