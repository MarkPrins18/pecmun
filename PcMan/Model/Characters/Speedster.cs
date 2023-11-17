using PcMan.Model;
using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System;

namespace PcMan.Model.Characters
{
    internal class Speedster : Character, IUpdatable, IViewable, ICanKill
    {
        private Character targetPlayer;
        private TimeSpan speedBoostDuration = TimeSpan.FromSeconds(0.5);
        private TimeSpan speedBoostCooldown = TimeSpan.FromSeconds(10);
        private TimeSpan oneStepDelay = TimeSpan.FromSeconds(1);
        private int speedBoostRange = 5;
        private int stepsDuringBoost = 4;

        private bool isSpeedBoostActive;
        private TimeSpan timeSinceSpeedBoostUsed;
        private TimeSpan timeSinceLastStep;

        public Speedster(int top, int left)
        { 
            Left = left;
            Top = top;

            this.targetPlayer = ((LevelScene)GameController.CurrentScene).GetPlayer();

            Symbol = "S";
            Color = ConsoleColor.Green;

            isSpeedBoostActive = false;
            timeSinceSpeedBoostUsed = TimeSpan.Zero;
            timeSinceLastStep = TimeSpan.Zero;
        }

        public Speedster()
        {
            Symbol = "S";
            Color = ConsoleColor.Green;

            targetPlayer = ((LevelScene)GameController.CurrentScene).GetPlayer();

            isSpeedBoostActive = false;
            timeSinceLastStep = TimeSpan.Zero;
            timeSinceSpeedBoostUsed = TimeSpan.Zero;

            PlaceCharacter();
            Move(0, 0);
        }

        public void Update(TimeSpan timeElapsed)
        {
            timeSinceLastStep += timeElapsed;
            if (isSpeedBoostActive) timeSinceSpeedBoostUsed += timeElapsed;

            // Check if Speedster is in range for a speed boost
            int distanceToPlayer = CalculateDistanceToPlayer();
            if (distanceToPlayer <= speedBoostRange && !isSpeedBoostActive && timeSinceSpeedBoostUsed >= speedBoostCooldown)
            {
                isSpeedBoostActive = true;
                timeSinceSpeedBoostUsed = TimeSpan.Zero;
            }

            TimeSpan currentDelay = isSpeedBoostActive ? speedBoostDuration / stepsDuringBoost : oneStepDelay;

            if (timeSinceLastStep >= currentDelay)
            {
                MoveTowardsPlayer();

                // Update timeSinceLastStep
                timeSinceLastStep -= currentDelay;
            }

            // Deactivate speed boost if the duration has passed
            if (isSpeedBoostActive && timeSinceSpeedBoostUsed >= speedBoostDuration)
            {
                isSpeedBoostActive = false;
            }
        }

        private void MoveTowardsPlayer()
        {
            int verticalDirection = Math.Sign(targetPlayer.Top - Top);
            int horizontalDirection = Math.Sign(targetPlayer.Left - Left);

            TryMove(verticalDirection, horizontalDirection);
        }

        private int CalculateDistanceToPlayer()
        {
            int rowDifference = Math.Abs(targetPlayer.Top - Top);
            int colDifference = Math.Abs(targetPlayer.Left - Left);

            return (int)Math.Sqrt(rowDifference * rowDifference + colDifference * colDifference);
        }
    }
}
