using RLEngine.Yaml.Serialization;
using RLEngine.Yaml.Utils;

using RLEngine.Games;
using RLEngine.Utils;

namespace RLEngine.Runner
{
    public static class Runner
    {
        public static void Main()
        {
            var gameContent = YamlDeserializer.Deserialize("Content");
            var propertyInfo = typeof(GameContent).GetPublicProperty(nameof(IIdentifiable.ID))!;
            propertyInfo.SetValue(gameContent, "ContentCopy");
            YamlSerializer.Serialize(gameContent);

            var game = new Game(gameContent);
            var gameView = new GameView(gameContent, 250);

            var log = game.SetupExample();
            gameView.Process(log);

            while (true)
            {
                var input = InputManager.GetInput(game);
                if (input == null) break;
                game.Input = input;
                log = game.ProcessTurns();
                gameView.Process(log);
            }
        }
    }
}