using RLEngine.Yaml.Serialization;
using RLEngine.Yaml.Utils;

using RLEngine.Core.Games;
using RLEngine.Core.Utils;

namespace RLEngine.CLI
{
    public static class Runner
    {
        public static void Main()
        {
            var gameContent = YamlDeserializer.Deserialize("../Content");
            YamlSerializer.Serialize(gameContent, "../ContentCopy");

            var game = new Game(gameContent);
            var gameView = new GameView(50);

            game.SetupExample();
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