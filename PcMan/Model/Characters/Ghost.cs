using PcMan.Controller;
using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Characters
{
    internal class Ghost : Character, IUpdatable, IViewable, ICanKill
    {
        int currentDirection;
        int rotationDirection;

        private int directionTop;
        private int directionLeft;

        private int timesMoved;

        int[] deltaTop = { -1, -1, 0, 1, 1, 1, 0, -1 };   // Up, up-right, right, down-right, down, down-left, left, up-left
        int[] deltaLeft = { 0, 1, 1, 1, 0, -1, -1, -1 };   // Up, up-right, right, down-right, down, down-left, left, up-left

        private ConsoleController consoleController;

        public Ghost()
        {
            Symbol = "☺";
            Color = ConsoleColor.Gray;

            Delay = new TimeSpan(0, 0, 0, 0, 800);
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            // Set the initial rotationDirection (1 or 0)
            rotationDirection = GameController.CurrentGame.RandomBetween(0, 2);
            chooseRandomDirection();

            PlaceCharacter();
        }

        public Ghost(int top, int left)
        {
            Symbol = "☺";
            Color = ConsoleColor.Gray;

            Delay = new TimeSpan(0, 0, 0, 0, 800);
            TimeElapsed = new TimeSpan(0, 0, 0, 0, 0);

            // Set the initial rotationDirection (1 or 0)
            rotationDirection = GameController.CurrentGame.RandomBetween(0, 2);
            chooseRandomDirection();

            Top = top;
            Left = left;
        }

        public Ghost(int top, int left, ConsoleController consoleController)
        {
            Top = top;
            Left = left;
            this.consoleController = consoleController;
        }

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

                    // increase timesMoved
                    timesMoved++;

                    // if timesMoved is 15, change direction
                    if (timesMoved == 5)
                    {
                        changeDirection();

                        // Set timesMoved to 0 - 5, so it appears a bit random
                        timesMoved = GameController.CurrentGame.RandomBetween(0, 5);
                    }
                }
                else
                {
                    changeDirection();
                }
            }
        }

        public override bool CanMove(int deltaTop, int deltaLeft)
        {
            // Allow the ghost to move through walls and off-screen
            return true;
        }

        public override bool TryMove(int deltaTop, int deltaLeft)
        {
            if (CanMove(deltaTop, deltaLeft))
            {
                Move(deltaTop, deltaLeft);
                return true;
            }
            // This should never happen, throw an exception
            throw new Exception("Ghost tried to move to an invalid position");
            return false;
        }


        public override void Move(int deltaTop, int deltaLeft)
        {
            // Move the ghost in the given direction.
            ((LevelScene) GameController.CurrentScene).Cells[Top, Left].LeaveCell(this);

            Top += deltaTop;
            Left += deltaLeft;

            // Wrap around the screen if the ghost goes off the edge.
            if (Left < 1)
            {
                Left = GameController.CurrentGame.GetWidth() - 2;
            }

            if (Left >= GameController.CurrentGame.GetWidth() - 1)
            {
                Left = 1;
            }
            if (Top < 1)
            {
                Top = GameController.CurrentGame.GetHeight() - 2;
            }
            if (Top >= GameController.CurrentGame.GetHeight() - 1)
            {
                Top = 1;
            }

            ((LevelScene)GameController.CurrentScene).Cells[Top, Left].EnterCell(this);
        }

        private void changeDirection()
        {
            // If rotationDirection is 1, rotate counterclockwise, otherwise rotate clockwise
            currentDirection += rotationDirection == 1 ? -1 : 1;

            // Handle wraparound if the new direction is out of bounds
            if (currentDirection < 0) currentDirection = deltaTop.Length - 1;
            if (currentDirection >= deltaTop.Length) currentDirection = 0;

            directionTop = deltaTop[currentDirection];
            directionLeft = deltaLeft[currentDirection];
        }

        private void chooseRandomDirection()
        {
            // Choose a random direction
            currentDirection = GameController.CurrentGame.RandomBetween(0, deltaTop.Length - 1);

            directionTop = deltaTop[currentDirection];
            directionLeft = deltaLeft[currentDirection];
        }
    }
}
