using RLEngine.Games;
using RLEngine.Utils;

using System;
using System.IO;

namespace RLEngine.Serialization.Yaml
{
    public static class YamlSerializer
    {
        public static void Serialize(IGameContent gameContent)
        {
            var serializationQueue = new SerializationQueue();
            var genericWritter = new GenericWritter(serializationQueue);

            Directory.CreateDirectory(gameContent.ID);
            Serialize(genericWritter, gameContent.ID, gameContent, typeof(IGameContent));
            while (serializationQueue.Count > 0)
            {
                var (element, type) = serializationQueue.Dequeue();
                var path = Path.Combine(gameContent.ID, SerializationPaths.Get(type));
                Serialize(genericWritter, path, element, type);
            }
        }

        private static void Serialize(GenericWritter genericWritter,
        string path, IIdentifiable element, Type type)
        {
            Directory.CreateDirectory(path);
            if (element.ID.Length == 0) throw new ArgumentNullException();
            var filename = element.ID + ".yml";
            using var sw = new StreamWriter(Path.Combine(path, filename));
            genericWritter.WriteObject(sw, element, type);
        }
    }
}