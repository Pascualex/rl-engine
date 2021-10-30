using RLEngine.Yaml.Serialization;
using RLEngine.Yaml.Utils;

using RLEngine.Core.Games;
using RLEngine.Core.Utils;

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
            var gameView = new GameView(50);

            var setupLogs = game.SetupExample();
            foreach (var log in setupLogs)
            {
                gameView.Process(log);
            }

            while (true)
            {
                if (game.ExpectsInput)
                {
                    var input = InputManager.GetInput(game);
                    if (input == null) break;
                    game.Input = input;
                }

                var log = game.ProcessStep();
                if (log != null) gameView.Process(log);
            }
        }
    }
}