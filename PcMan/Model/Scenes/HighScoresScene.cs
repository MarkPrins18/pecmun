using PcMan.Controller;
using PcMan.View;

// Namespace for the PcMan game model scenes
namespace PcMan.Model.Scenes
{
    // HighScoresScene is responsible for displaying the high scores screen in the game
    internal class HighScoresScene : Scene
    {
        private int colorIndex;
        private ConsoleColor[] colors;
        private TimeSpan timeElapsed;
        private List<HighScoreEntry> highScores;

        /// <summary>
        /// Initializes a new instance of the HighScoresScene class.
        /// </summary>
        /// <param name="gameController">A reference to the game controller.</param>
        /// <param name="consoleController">A reference to the console controller.</param>
        /// <param name="consoleView">A reference to the console view.</param>
        public HighScoresScene(GameController gameController, ConsoleController consoleController, ConsoleView consoleView) : base(gameController, consoleController, consoleView)
        { 
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

            highScores = GameController.CurrentGame.highScoreData.GetHighScores();
        }

        /// <summary>
        /// Updates the HighScoresScene based on the time passed since the last update.
        /// </summary>
        /// <param name="deltaTime">The time passed since the last update.</param>
        public override void Update(TimeSpan deltaTime)
        {
            // Show the high scores.
            timeElapsed += deltaTime;
            
            // Update colors every 0.25 seconds
            if (timeElapsed.TotalSeconds > .25)
            {
                timeElapsed -= TimeSpan.FromSeconds(.25);
                colorIndex = (colorIndex + 1) % 7; // Cycle through 7 colors
                DisplayHighScores();
            }
            
            // If the user presses Enter, set the state to GameState.MainMenu
            // and return.
            if (consoleController.GetLastKey(true) == ConsoleKey.Enter)
            {
                gameController.SetState(PcMan.Model.GameController.GameState.MainMenu);
                return;
            }
        }

        /// <summary>
        /// Displays the high scores on the console view.
        /// </summary>
        private void DisplayHighScores()
        {
            consoleView.Show("HIGH SCORES", 1, 1, ConsoleColor.Yellow);

            int startY = (gameController.Height - highScores.Count) / 2;

            for (int i = 0; i < highScores.Count; i++)
            {
                HighScoreEntry entry = highScores[i];
                ConsoleColor color = colors[(colorIndex + i) % colors.Length];
                int topPosition = startY + i;

                consoleView.Show((i + 1).ToString() + ". " + entry.PlayerName, topPosition, 0, color);
                consoleView.Show(entry.Score.ToString(), topPosition, 1, color);
                consoleView.Show("Level " + entry.Level.ToString(), topPosition, 2, color);
            }

            consoleView.Show("Press ENTER to return to the main menu", gameController.Height - 2, 1, ConsoleColor.White);
        }
    }
}