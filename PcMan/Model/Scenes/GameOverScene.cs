using PcMan.Controller;
using PcMan.View;
using System;
using System.Collections.Generic;

namespace PcMan.Model.Scenes
{
    internal class GameOverScene : Scene
    {
        private int colorIndex;
        private ConsoleColor[] colors;
        private TimeSpan timeElapsed;

        public GameOverScene(GameController gameController, ConsoleController consoleController, ConsoleView consoleView) : base(gameController, consoleController, consoleView)
        {
            this.gameController = gameController;
            this.consoleController = consoleController;
            this.consoleView = consoleView;

            timeElapsed = TimeSpan.Zero;

            colorIndex = 0;
            colors = new ConsoleColor[]
            {
            ConsoleColor.Red,
            ConsoleColor.DarkYellow,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Blue,
            ConsoleColor.Magenta
            };
        }

        public override void Update(TimeSpan deltaTime)
        {
            // Show the "GAME OVER!" text.
            timeElapsed += deltaTime;

            // Update colors every 0.25 seconds
            if (timeElapsed.TotalSeconds > .25)
            {
                timeElapsed -= TimeSpan.FromSeconds(.25);
                colorIndex = (colorIndex + 1) % 7; // Cycle through 7 colors
                DisplayGameOver();
            }

            // If the user presses Enter, set the state to GameState.MainMenu
            // and return.
            if (consoleController.GetLastKey(true) == ConsoleKey.Enter)
            {
                gameController.SetState(PcMan.Model.GameController.GameState.MainMenu);
                return;
            }
        }

        private void DisplayGameOver()
        {
            string[] gameOverText = new string[]
            {
            "  ____                         ___",
            " / ___| __ _ _ __ ___   ___   / _ \\__   _____ _ __",
            "| |  _ / _` | '_ ` _ \\ / _ \\ | | | \\ \\ / / _ \\ '__|",
            "| |_| | (_| | | | | | |  __/ | |_| |\\ V /  __/ |",
            " \\____|\\__,_|_| |_| |_|\\___|  \\___/  \\_/ \\___|_|"
            };

            int startX = (gameController.Width - gameOverText[0].Length) / 2;
            int startY = (gameController.Height - gameOverText.Length) / 2;

            for (int i = 0; i < gameOverText.Length; i++)
            {
                ConsoleColor color = colors[(colorIndex + i) % colors.Length];
                consoleView.Show(gameOverText[i], startY + i, startX, color);
            }

            consoleView.Show("Press ENTER to return to the main menu", gameController.Height - 2, 1, ConsoleColor.White);
        }
    }
}
