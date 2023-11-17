using PcMan.Model.Interfaces;
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
    internal class Scanner : Character, IUpdatable, IViewable, ICanKill
    {
        // Declare all variables needed
        private int directionTop = 1;
        private int directionLeft = 1;

        public Scanner()
        {

            Delay = new TimeSpan(0, 0, 0, 0, 100);
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            // Place character
            PlaceCharacter();

            // Set characters symbol
            Symbol = "X";

            // Set characters color
            Color = ConsoleColor.Cyan;

            Move(0, 0);
        }

        public Scanner(int top, int left)
        {
            Delay = new TimeSpan(0, 0, 0, 0, 100);
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            // Place character
            Top = top;
            Left = left;

            // Set characters symbol
            Symbol = "X";

            // Set characters color
            Color = ConsoleColor.Cyan;

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
                TimeElapsed -= Delay;
                // Try moving horizontally. If that fails,
                // reverse the horizontal direction, and move vertically.
                // If moving vertically fails, reverse the vertical direction
                // and move horizontally. Throw an exception if that fails
                // as well.
                if (!TryMove(0, directionLeft))
                {
                    directionLeft *= -1;
                    if (!TryMove(directionTop, 0))
                    {
                        directionTop *= -1;
                        if (!TryMove(0, directionLeft))
                        {
                            if (!TryMove(directionTop, 0))
                            {
                                throw new Exception("Scanner is stuck");
                            }
                        }
                    }
                }


            }
        }

        private void changeDirection()
        {
            bool isMoving = false;

            while (!isMoving)
            {
                directionTop = GameController.CurrentGame.RandomBetween(0, 3) - 1;
                directionLeft = GameController.CurrentGame.RandomBetween(0, 3) - 1;

                if (directionLeft != 0 || directionTop != 0)
                {
                    isMoving = true;
                }
            }
        }
    }
}
