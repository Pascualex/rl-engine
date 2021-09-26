using RLEngine.Serialization.TypeConverters;
using RLEngine.Serialization.Abilities;
using RLEngine.Serialization.Boards;
using RLEngine.Serialization.Entities;
using RLEngine.Serialization.Utils;

using RLEngine.Utils;

using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace RLEngine.Serialization
{
    public class ContentSerializer
    {
        private readonly ISerializer serializer;
        private readonly IDeserializer deserializer;

        public ContentSerializer()
        {
            var actionAmountTC = new ActionAmountTypeConverter();
            var effectTC = new IEffectTypeConverter(actionAmountTC);
            var abilityTC = new IAbilityTypeConverter(effectTC);

            serializer = new SerializerBuilder()
                .WithTypeConverter(actionAmountTC)
                .WithTypeConverter(effectTC)
                .WithTypeConverter(abilityTC)
                .Build();

            deserializer = new DeserializerBuilder()
                .WithTypeConverter(actionAmountTC)
                .WithTypeConverter(effectTC)
                .WithTypeConverter(abilityTC)
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

        public GameContent Deserialize(string path)
        {
            var boardSize = new Size(10, 10);
            var floorType = new TileType() { Name = "Floor" };
            var wallType = new TileType { Name = "Wall", BlocksGround = true, BlocksAir = true };
            var playerType = new EntityType { Name = "Pascu", IsAgent = true };
            var goblinType = new EntityType { Name = "Goblin", IsAgent = true };
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
                contentElement.ID = Path.GetFileName(filepath);
                yield return contentElement;
            }
        }
    }
}