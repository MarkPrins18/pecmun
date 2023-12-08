using PcMan.Model;
using PcMan.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Controller
{
    /// <summary>
    /// ConsoleController class handles user input and provides methods to access the input data.
    /// </summary>
    public class ConsoleController : IUpdatable
    {
        private string lastPressed;
        private ConsoleKey lastKey;

        /// <summary>
        /// Update the state of the ConsoleController, capturing user input and handling music and pause controls.
        /// </summary>
        /// <param name="deltaTime">The elapsed time since the last update call.</param>
        public void Update(TimeSpan deltaTime)
        {
            // If a key was preesed, handle the keypress & controller keys.
            if (Console.KeyAvailable)
            {
                handleKeyPress();
                handleControllerKeys();
            }
        }

        /// <summary>
        /// Handles keypress events, capturing the last arrow key pressed as a string.
        /// </summary>
        private void handleKeyPress()
        {
            lastKey = Console.ReadKey(true).Key;

            if (lastKey == ConsoleKey.UpArrow)
            {
                lastPressed = "ArrowUp";
            }
            else if (lastKey == ConsoleKey.DownArrow)
            {
                lastPressed = "ArrowDown";
            }
            else if (lastKey == ConsoleKey.LeftArrow)
            {
                lastPressed = "ArrowLeft";
            }
            else if (lastKey == ConsoleKey.RightArrow)
            {
                lastPressed = "ArrowRight";
            }
        }

        /// <summary>
        /// Handles special controller keys such as music controls and pause.
        /// </summary>
        private void handleControllerKeys()
        {
            // If lastKey == Pause, toggle the pause state (GameController.TogglePause();)
            if (lastKey == ConsoleKey.Pause)
            {
                GameController.CurrentGame.TogglePause();
            }
        }

        /// <summary>
        /// Returns the last pressed arrow key as a string.
        /// </summary>
        /// <returns>A string representing the last pressed arrow key.</returns>
        public string GetLastPressedString()
        {
            return lastPressed;
        }

        // TODO
        /// <summary>
        /// [DEPRECATED] This method is deprecated. Use GetLastKey instead. 
        /// Returns the last pressed arrow key as a string, and optionally resets the lastPressed value.
        /// </summary>
        /// <param name="resetKey">Whether to reset the lastPressed value after returning it.</param>
        /// <returns>A string representing the last pressed arrow key.</returns>
        public string GetLastPressedString(bool resetKey)
        {
            if(resetKey)
            {
                string tmp = lastPressed;
                lastPressed = "";
                return tmp;
            }

            return GetLastPressedString();
        }

        /// <summary>
        /// Returns the last pressed ConsoleKey.
        /// </summary>
        /// <returns>The last pressed ConsoleKey.</returns>
        public ConsoleKey GetLastKey()
        {
            return lastKey;
        }

        /// <summary>
        /// Returns the last pressed ConsoleKey, and optionally resets the lastKey value.
        /// </summary>
        /// <param name="resetKey">Whether to reset the lastKey value after returning it.</param>
        /// <returns>The last pressed ConsoleKey.</returns>
        public ConsoleKey GetLastKey(bool resetKey)
        {
            if (resetKey)
            {
                ConsoleKey tmp = lastKey;
                lastKey = 0;
                return tmp;
            }

            return GetLastKey();
        }

        /// <summary>
        /// Determines if a key has been pressed.
        /// Important: Only "works" if resetKey is properly set, everywhere it needs to be...
        /// </summary>
        /// <returns>Returns true if a key has been pressed, otherwise false.</returns>
        public bool WasKeyPressed()
        {
            return lastKey != 0;
        }
    }
}
