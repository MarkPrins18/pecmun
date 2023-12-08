using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Characters
{
    internal class LaserTurret : Character, IViewable, IUpdatable
    {
        Direction direction;
        public LaserTurret()
        {
            PlaceCharacter();

            Color = ConsoleColor.Red;
            Symbol = "L";

            Delay = TimeSpan.FromSeconds(3);
            TimeElapsed = TimeSpan.Zero;
            setDirection();
            Move(0, 0);
        }

        // Constructor with params for position
        public LaserTurret(int top, int left)
        {
            Top = top;
            Left = left;

            Color = ConsoleColor.Red;
            Symbol = "L";

            Delay = TimeSpan.FromSeconds(3);
            TimeElapsed = TimeSpan.Zero;
            setDirection();
            Move(0, 0);
        }

        public void Shoot()
        {
            int laserTop = Top;
            int laserLeft = Left;

            bool hitWall = false;
            while (!hitWall)
            {
                // Move laser one step in direction
                laserTop += direction.getDirection()[0];
                laserLeft += direction.getDirection()[1];

                // Check if laser hit wall
                if (((LevelScene)GameController.CurrentScene).GetCell(laserTop, laserLeft) != null &&
                    ((LevelScene)GameController.CurrentScene).GetCell(laserTop, laserLeft).CanEnter())
                {
                    // Create a new laser on this position
                    ((LevelScene)GameController.CurrentScene).AddUpdatable(new Laser(laserTop, laserLeft, direction));
                }
                else
                {
                    hitWall = true;
                }
            }

            setDirection();

        }

        private void setDirection()
        {
            // Create a random new direction
            direction = new Direction(1);

            // Set our symbol to an arrow, representing our direction
            switch (direction.getDirection()[0])
            {
                case 1:
                    Symbol = "v";
                    break;
                case -1:
                    Symbol = "^";
                    break;
                case 0:
                    switch (direction.getDirection()[1])
                    {
                        case 1:
                            Symbol = ">";
                            break;
                        case -1:
                            Symbol = "<";
                            break;
                    }
                    break;
            }

            // Move to make sure our visual get's updated.
            Move(0, 0);

        }

        public void Update(TimeSpan timeElapsed)
        {
            // Add timeElapsed to TimeElapsed
            TimeElapsed += timeElapsed;

            // If TimeElapsed is greater than Delay
            if (TimeElapsed > Delay)
            {
                // Reset TimeElapsed
                TimeElapsed = TimeSpan.Zero;

                // Shoot
                Shoot();
            }
        }
    }
}
