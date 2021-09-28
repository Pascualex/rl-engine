using RLEngine.Serialization.Games;
using RLEngine.Serialization.Abilities;
using RLEngine.Serialization.Boards;
using RLEngine.Serialization.Entities;
using RLEngine.Serialization.Utils;

using RLEngine.Utils;

using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization.Yaml
{
    public class GameContentYamlDeserializer
    {
        private readonly IDeserializer deserializer;

        public GameContentYamlDeserializer()
        {
            deserializer = new DeserializerBuilder()
                .WithTypeConverter(new CustomDeserializer())
                .Build();
        }

        public GameContent Deserialize(string path)
        {
            var boardSize = new Size(10, 10);
            var floorType = new TileType() { ID = "Floor", Name = "Floor" };
            var wallType = new TileType { ID = "Wall", Name = "Wall", BlocksGround = true, BlocksAir = true };
            var humanType = new EntityType { ID = "Human", Name = "Human", IsAgent = true };
            var playerType = new EntityType
            { ID = "Hero", Name = "Hero", MaxHealth = 200, Parent = humanType };
            var goblinType = new EntityType { ID = "Goblin", Name = "Goblin", IsAgent = true };
            Ability? ability = null;
            foreach (var serializedAbility in Deserialize<Ability>(Path.Combine(path, "Abilities")))
            {
                ability = serializedAbility;
            }
            return new GameContent
            (
                boardSize,
                floorType,
                wallType,
                playerType,
                goblinType,
                ability!
            );
        }

        public IEnumerable<T> Deserialize<T>(string path) where T : IDeserializable
        {
            foreach (var filepath in Directory.GetFiles(path))
            {
                var yamlString = File.ReadAllText(filepath);
                var contentElement = deserializer.Deserialize<T>(yamlString);
                contentElement.ID = Path.GetFileNameWithoutExtension(filepath);
                yield return contentElement;
            }
        }
    }
}