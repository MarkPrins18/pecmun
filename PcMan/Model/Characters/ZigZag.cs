using PcMan.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Characters
{
    internal class ZigZag : Character, IUpdatable, IViewable, ICanKill
    {
        private int directionTop;
        private int directionLeft;

        private int moves;
        
        public ZigZag()
        {
            Delay = new TimeSpan(0, 0, 0, 0, 300);
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            // Set random direction
            changeDirection();

            // Place character
            PlaceCharacter();

            // Set characters symbol
            Symbol = "Z";

            // Set characters color
            Color = ConsoleColor.Cyan;

        }

        // Constructor with params for position
        public ZigZag(int top, int left)
        {
            Delay = new TimeSpan(0, 0, 0, 0, 300);
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            // Set random direction
            changeDirection();

            // Place character
            Top = top;
            Left = left;

            // Set characters symbol
            Symbol = "Z";

            // Set characters color
            Color = ConsoleColor.Cyan;

        }

        private void changeDirection()
        {
            // Set directionTop and directionLeft to 1 or -1
            // (directionTop and directionLeft should not be 0)
            directionTop = 0;
            directionLeft = 0;

            while (directionTop == 0 )
            {
                directionTop = GameController.CurrentGame.RandomBetween(-1, 2);
            }

            while (directionLeft == 0)
            {
                directionLeft = GameController.CurrentGame.RandomBetween(-1, 2);
            }
        }

        public void Update(TimeSpan timeElapsed)
        {
            // ZigZag has to move 5 steps diagonally
            // After 5 steps, change direction
            TimeElapsed += timeElapsed;

            if (TimeElapsed > Delay)
            {
                TimeElapsed -= Delay;

                // Move character
                if(TryMove(directionLeft, directionTop))
                {
                    moves++;

                    if (moves == 5)
                    {
                        changeDirection();
                    }
                }
                else
                {
                    moves = 0;
                    changeDirection();
                }
            }
        }
    }
}
