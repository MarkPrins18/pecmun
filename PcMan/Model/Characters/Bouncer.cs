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
    internal class Bouncer : Character, IUpdatable, IViewable, ICanKill
    {
        // Declare all variables needed
        private int directionTop;
        private int directionLeft;

        public Bouncer()
        {

            Delay = new TimeSpan(0, 0, 0, 0, 300);
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            changeDirection();

            if (directionTop > 1 || directionLeft > 1 || directionTop < -1 || directionLeft < -1 || directionLeft == 0 && directionTop == 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(directionTop + "," + directionLeft);

                throw new Exception("Invalid direction");
            }

            // Place character
            PlaceCharacter();

            // Set characters symbol
            Symbol = "B";

            // Set characters color
            Color = ConsoleColor.DarkYellow;

            Move(0, 0);
        }

        public Bouncer(int top, int left)
        {

            Delay = new TimeSpan(0, 0, 0, 0, 300);
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            changeDirection();

            if (directionTop > 1 || directionLeft > 1 || directionTop < -1 || directionLeft < -1 || directionLeft == 0 && directionTop == 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(directionTop + "," + directionLeft);

                throw new Exception("Invalid direction");
            }

            // Place character
            Top = top;
            Left = left;

            // Set characters symbol
            Symbol = "B";

            // Set characters color
            Color = ConsoleColor.DarkYellow;

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
                bool moved = TryMove(directionTop, directionLeft);

                if (moved)
                {
                    // decrease timeElapsed with delay
                    TimeElapsed -= Delay;
                }
                else
                {
                    changeDirection();
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
