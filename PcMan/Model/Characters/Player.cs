using PcMan.Controller;
using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Characters
{
    /// <summary>
    /// Player class, derived from Character class
    /// </summary>
    public class Player : Character, IUpdatable, IViewable
    {
        // Declare all variables needed
        private ConsoleController keyboardController;
        private List<ICollectable> collectedItems;

        public int Score;
        public int Lives;

        public Player(int top, int left, ConsoleController keyboardController)
        {
            // Set variables to the params
            Left = left;
            Top = top;

            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            Delay = new TimeSpan(0, 0, 0, 0, 10);

            this.keyboardController = keyboardController;

            // Set characters symbol
            Symbol = "@";

            // Set characters color
            Color = ConsoleColor.White;

            Move(0, 0);
        }

        /// <summary>
        /// Update, gets called continuously while the game is playing
        /// </summary>
        public void Update(TimeSpan timeElapsed)
        {
            TimeElapsed += timeElapsed;

            if (TimeElapsed > Delay)
            {
                bool moved = false;
                // Ket last pressed key from KeyboardController, reset the
                // lastPressedKey to null.
                string lastPressed = keyboardController.GetLastPressedString(true);

                // If up is pressed, move up
                if (lastPressed == "ArrowUp")
                {
                    moved = TryMove(-1, 0);
                }
                // If down is pressed, move down
                else if (lastPressed == "ArrowDown")
                {
                    moved = TryMove(1, 0);
                }
                // If right is pressed, move right
                else if (lastPressed == "ArrowRight")
                {
                    moved = TryMove(0, 1);
                }
                // If left is pressed, move left
                else if (lastPressed == "ArrowLeft")
                {
                    moved = TryMove(0, -1);
                }

                if (moved)
                {
                    // reset TimeElapsed
                    TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);
                }
            }
        }

        public void Kill()
        {

            // Check if we have lives left. If we do, remove one life and
            // reset the player to the start position.
            if (Lives > 0)
            {
                Lives--;
                Left = 1;
                Top = 1;
            }
            // If we don't have lives left, end the game.
            else
            {
                // If we don't have lives left, end the game.
                ((LevelScene)GameController.CurrentScene).GameOver();
            }            
        }

        internal List<ICollectable> GetCollectedItems()
        {
            return collectedItems;
        }

        internal void AddCollectedItem(ICollectable collectable)
        {
            if (collectedItems == null)
            {
                collectedItems = new List<ICollectable>();
            }

            collectedItems.Add(collectable);
        }

        public void ClearCollectedItems()
        {
            collectedItems = new List<ICollectable>();
        }

        public void AddScore(int score)
        {
            Score += score;
        }

        public void AddLives(int lives)
        {
            Lives += lives;
        }

        public void RemoveLives(int lives)
        {
            Lives -= lives;
        }

        public void Reset()
        {
            Left = 1;
            Top = 1;
        }

        public void ResetScore()
        {
            Score = 0;
        }

        public void ResetLives()
        {
            Lives = 3;
        }

        public override bool CanMove(int deltaTop, int deltaLeft)
        {
            // Check if new position is valid
            if (
                    Top + deltaTop < 0 ||
                    Top + deltaTop > GameController.CurrentGame.GetHeight() - 1 ||
                    Left + deltaLeft < 0 ||
                    Left + deltaLeft > GameController.CurrentGame.GetWidth() - 1)
            {
                //Console.SetCursorPosition(130, 10);
                //Console.WriteLine("You can't go there, out of bounds!");
                return false;
            }

            // Check if there is a cell in the Game cell-array
            if (((LevelScene)GameController.CurrentScene).Cells[Top + deltaTop, Left + deltaLeft] != null)
            {
                // Check if the cell is enterable
                if (((LevelScene)GameController.CurrentScene).Cells[Top + deltaTop, Left + deltaLeft].CanEnter())
                {
                    //Console.SetCursorPosition(130, 10);
                    //Console.WriteLine("You can go there.                    ");
                    return true;
                }
                else
                {
                    //Console.SetCursorPosition(130, 10);
                    //Console.WriteLine("You can't go there, there is a wall!");
                    return false;
                }
            }

            //Console.SetCursorPosition(130, 10);
            //Console.WriteLine("You can't go there, there is no cell!");
            return false;
        }


    }
}
