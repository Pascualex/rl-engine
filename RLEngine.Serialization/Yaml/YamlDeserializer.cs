using RLEngine.Serialization.Games;
using RLEngine.Serialization.Yaml.Utils;
using RLEngine.Serialization.Utils;

using System;
using System.IO;

namespace RLEngine.Serialization.Yaml
{
    public static class YamlDeserializer
    {
        public static GameContent Deserialize(string path)
        {
            var serializationQueue = new SerializationQueue<Deserializable>();
            var reader = new GenericReader(serializationQueue);

            var gameContentID = Path.GetFileName(path);
            var gameContent = new GameContent { ID = gameContentID };
            Deserialize(reader, path, gameContent, typeof(GameContent));
            while (serializationQueue.Count > 0)
            {
                var (target, type) = serializationQueue.Dequeue();
                var typePath = Path.Combine(gameContent.ID, SerializationPaths.Get(type));
                Deserialize(reader, typePath, target, type);
            }
            return gameContent;
        }

        public static void Deserialize(GenericReader reader,
        string path, Deserializable target, Type type)
        {
            if (target.ID.Length == 0) throw new ArgumentNullException();
            if (target.ID == DefaultID.Value) throw new ArgumentNullException();
            var filename = target.ID + ".yml";
            using var streamReader = new StreamReader(Path.Combine(path, filename));
            reader.ReadObject(streamReader, type, target);
        }
    }
}