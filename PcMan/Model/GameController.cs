using PcMan.Controller;
using PcMan.Model.Scenes;
using PcMan.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model
{
    /// <summary>
    /// GameController class manages the game flow, state changes, and initialization of scenes.
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// Enum to represent the different game states.
        /// </summary>
        public enum GameState
        {
            MainMenu,
            SelectLevel,
            Playing,
            GameOver,
            NextLevel,
            HighScores,
            NewHighScore,
            Exiting,
            Closed
        }

        // Singleton to store the current scene
        public static Scene CurrentScene;

        // Singleton to store the current game controller
        public static GameController CurrentGame;

        // A bool to to indicate if the game state has changed last update
        private bool stateChanged = false;

        // Field for the current game state
        private GameState currentState;

        // Ints for game with and height
        public int Height;
        public int Width;

        // For RNG trhoughout the game
        private Random random;

        // Controllers & views
        private ConsoleController consoleController;
        private ConsoleView consoleView;

        // High score data storage
        public HighScoreData highScoreData;

        // Fields for update timing
        private TimeSpan timeElapsed;
        private DateTime startTime;
        private DateTime endTime;

        // FIXME: This shouldn't be done like this, this is something we should retrieve
        // from the level after playing it, but in the current gameController setup, there
        // is now way to do this, since the levelScene (stored in CurrentScene) has already
        // been overwritten by the next scene.
        // Fields to store the last score and level.
        public int lastScore;
        public int lastLevel;

        public GameController()
        {
            // Set the game width and height. Thould be done before initializing the console view.
            Height = 30;
            Width = 40;

            // Initialize the controllers & console view
            consoleController = new ConsoleController();
            consoleView = new ConsoleView(Height, Width);

            // Initialize the random number generator
            random = new Random();

            // Initialize the high score data
            highScoreData = new HighScoreData();

            // Set the current game controller to this
            CurrentGame = this;
        }

        public void SetWindow(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            consoleView = new ConsoleView(Height, Width);
        }
        public void Play()
        {
            // Start with the main menu
            SetState(GameState.MainMenu);

            while (currentState != GameState.Closed)
            {
                // Calculate how long previous update took
                timeElapsed = endTime - startTime;

                // Register start of "update"
                startTime = DateTime.Now;

                // If the game state has changed, initialize the new scene and reset stateChanged
                if (stateChanged)
                {
                    handleStateChanged();
                }
                else // Only call updates if we didn't change scenes this update
                {
                    // Update console controller and current scene with the elapsed time
                    consoleController.Update(timeElapsed);
                    CurrentScene.Update(timeElapsed);

                    // Register end of "update"
                    endTime = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Handles the state change of the game.
        /// This method is responsible for clearing the current scene and screen,
        /// creating a new scene based on the current game state, and resetting the stateChanged flag.
        /// </summary>
        private void handleStateChanged()
        {
            // Clear the current scene
            // TODO: Check wether this is gc'ed properly
            CurrentScene = null;

            // Clear the screen
            consoleView.ClearScreen();

            // Create the new scene, based on the current game state
            switch (currentState)
            {
                case GameState.MainMenu:
                    CurrentScene = new MainMenuScene(this, consoleController, consoleView);
                    break;
                case GameState.SelectLevel:
                    CurrentScene = new SelectLevelScene(this, consoleController, consoleView);
                    break;
                case GameState.Playing:
                    CurrentScene = new LevelScene(this, consoleController, consoleView, 1);
                    ((LevelScene)CurrentScene).SetupLevel();
                    break;
                case GameState.HighScores:
                    CurrentScene = new HighScoresScene(this, consoleController, consoleView);
                    break;
                case GameState.NewHighScore:
                    // TODO: This should provide NewHighScoreScene with score & level
                    CurrentScene = new NewHighScoreScene(this, consoleController, consoleView);
                    break;
                case GameState.Exiting:
                    CurrentScene = new ExitScene(this, consoleController, consoleView);
                    break;
                case GameState.Closed:
                    break;
            }
            // Reset stateChanged
            stateChanged = false;

            // Register end of "update"
            endTime = DateTime.Now;
        }

        /// <summary>
        /// Sets the game state to the given state.
        /// </summary>
        /// <param name="newState">The new game state.</param>
        public void SetState(GameState newState)
        {
            currentState = newState;
            stateChanged = true;
        }

        /// <summary>
        /// Generates a random integer between a and b (inclusive).
        /// </summary>
        /// <param name="a">The lower bound.</param>
        /// <param name="b">The upper bound.</param>
        /// <returns>A random integer between a and b (inclusive).</returns>
        public int RandomBetween(int a, int b)
        {
            return random.Next(a, b);
        }

        /// <summary>
        /// Generates a random float between a and b (inclusive).
        /// </summary>
        /// <param name="a">The lower bound.</param>
        /// <param name="b">The upper bound.</param>
        /// <returns>A random float between a and b (inclusive).</returns>
        public float RandomBetween(float a, float b)
        {
            return (float)random.NextDouble() * (b - a) + a;
        }

        /// <summary>
        /// Gets the game height.
        /// </summary>
        /// <returns>The height of the game.</returns>
        public int GetHeight()
        {
            return Height;
        }

        /// <summary>
        /// Gets the game width.
        /// </summary>
        /// <returns>The width of the game.</returns>
        public int GetWidth()
        {
            return Width;
        }

        /// <summary>
        /// Toggles the pause state of the game (currently empty).
        /// </summary>
        internal void TogglePause()
        {
            
        }
    }
}
