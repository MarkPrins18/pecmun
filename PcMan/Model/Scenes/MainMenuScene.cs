using PcMan.Controller;
using PcMan.View;
using static PcMan.Model.GameController;

namespace PcMan.Model.Scenes
{
    internal class MainMenuScene : Scene
    {
        public MainMenuScene(GameController gameController, ConsoleController consoleController, ConsoleView consoleView) : base(gameController, consoleController, consoleView)
        {
            this.gameController = gameController;
            this.consoleController = consoleController;
            this.consoleView = consoleView;
        }

        public override void Update(TimeSpan deltaTime)
        {
            consoleView.Show("Welcome to PcMan!", 10, 1, ConsoleColor.Red);

            // Show the options:
            // 1. Start new game
            // 2. Select level
            // 3. High scores
            // X. Exit

            consoleView.Show("1. Start new game", 12, 1, ConsoleColor.White);
            consoleView.Show("2. Select level", 13, 1, ConsoleColor.White);
            consoleView.Show("3. High scores", 14, 1, ConsoleColor.White);
            consoleView.Show("X. Exit", 15, 1, ConsoleColor.White);

            ConsoleKey lastKey = consoleController.GetLastKey(true);

            if (lastKey == ConsoleKey.D1)
            {
                gameController.SetState(GameState.Playing);
            }
            else if (lastKey == ConsoleKey.D2)
            {
                gameController.SetState(GameState.SelectLevel);
            }
            else if (lastKey == ConsoleKey.D3) 
            {
                gameController.SetState(GameState.HighScores);
            }
            else if (lastKey == ConsoleKey.X)
            {
                gameController.SetState(GameState.Exiting);
            }
        }
    }
}