using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Characters
{
    internal class Laser : Character, IUpdatable, ICanKill
    {
        private Direction direction;
        public Laser(int top, int left, Direction direction)
        {
            Top = top;
            Left = left;
            Color = ConsoleColor.Red;
            Symbol = "=";
            this.direction = direction;

            // Set delay to 1second
            Delay = TimeSpan.FromSeconds(1);
            TimeElapsed = TimeSpan.FromSeconds(0);

            Move(0, 0);
        }

        public void Update(TimeSpan timeElapsed)
        {
            TimeElapsed += timeElapsed;
            if (TimeElapsed > Delay)
            {
                // Remove laser from cell
                ((LevelScene)GameController.CurrentScene).GetCell(Top, Left).LeaveCell(this);

                // Remove laser from game
                ((LevelScene)GameController.CurrentScene).RemoveUpdatable(this);
            }
        }

        public override string GetImage()
        {
            if (direction.IsVertical())
            {
                return "│";
            }
            else
            {
                return "─";
            }
        }
    }
}
