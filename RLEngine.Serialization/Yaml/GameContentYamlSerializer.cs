using RLEngine.Serialization.Utils;

using RLEngine.Games;
using RLEngine.Utils;

using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeResolvers;

namespace RLEngine.Serialization.Yaml
{
    public static class GameContentYamlSerializer
    {
        public static void Serialize(IGameContent gameContent)
        {
            var serializationQueue = new SerializationQueue();
            var serializer = new SerializerBuilder()
                .WithTypeConverter(new CustomSerializer(serializationQueue))
                .WithTypeResolver(new StaticTypeResolver())
                .Build();

            Directory.CreateDirectory(gameContent.ID);
            Serialize(serializer, gameContent.ID, gameContent, typeof(IGameContent));
            while (serializationQueue.Count > 0)
            {
                var (element, type) = serializationQueue.Dequeue();
                var path = Path.Combine(gameContent.ID, SPaths.Get(type));
                Serialize(serializer, path, element, type);
            }
        }

        private static void Serialize(ISerializer serializer,
        string path, IIdentifiable element, Type type)
        {
            Directory.CreateDirectory(path);
            if (element.ID.Length == 0) throw new ArgumentNullException();
            var filename = element.ID + ".yml";
            using var sw = new StreamWriter(Path.Combine(path, filename));
            serializer.Serialize(sw, element, type);
        }
    }
}