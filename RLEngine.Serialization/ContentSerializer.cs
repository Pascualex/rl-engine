using RLEngine.Serialization.Yaml;
using RLEngine.Serialization.Utils;

using RLEngine.Utils;

using System;
using System.IO;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization
{
    public class ContentSerializer
    {
        private readonly ISerializer serializer;

        public ContentSerializer()
        {
            serializer = new SerializerBuilder()
                .WithTypeConverter(new CustomTypeConverter())
                .Build();
        }

        public void Serialize(IGameContent gameContent, string path)
        {
            Directory.CreateDirectory(path);
            Serialize(Path.Combine(path, SPaths.TilesTypes), gameContent.FloorType);
            Serialize(Path.Combine(path, SPaths.TilesTypes), gameContent.WallType);
            Serialize(Path.Combine(path, SPaths.Abilities), gameContent.Ability);
        }

        private void Serialize(string path, IIdentifiable contentElement)
        {
            Directory.CreateDirectory(path);
            var yamlString = serializer.Serialize(contentElement);
            if (contentElement.ID.Length == 0) throw new ArgumentNullException();
            var filename = contentElement.ID + ".yml";
            File.WriteAllText(Path.Combine(path, filename), yamlString);
        }
    }
}