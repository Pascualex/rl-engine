using RLEngine.Serialization.Yaml;

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

        public void Serialize(GameContent gameContent, string path)
        {
            Directory.CreateDirectory(path);
            Serialize(Path.Combine(path, "Abilities"), gameContent.Ability);
        }

        private void Serialize(string path, IIdentifiable contentElement)
        {
            Directory.CreateDirectory(path);
            var yamlString = serializer.Serialize(contentElement);
            if (contentElement.ID.Length == 0) throw new ArgumentNullException();
            File.WriteAllText(Path.Combine(path, contentElement.ID), yamlString);
        }
    }
}