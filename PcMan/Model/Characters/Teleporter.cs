using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Characters
{
    internal class Teleporter : Character, IUpdatable, IViewable, ICanKill
    {
        public Teleporter()
        {
            // Set the symbol
            Symbol = "T";

            // Set the color of the Teleporter
            Color = ConsoleColor.Magenta;

            TimeElapsed = TimeSpan.FromSeconds(0);
            Delay = TimeSpan.FromSeconds(1);

            PlaceCharacter();

        }

        public Teleporter(int top, int left)
        {
            // Set the symbol
            Symbol = "T";

            // Set the color of the Teleporter
            Color = ConsoleColor.Magenta;

            TimeElapsed = TimeSpan.FromSeconds(0);
            Delay = TimeSpan.FromSeconds(1);

            Top = top;
            Left = left;
        }

        public void Update(TimeSpan timeElapsed)
        {
            TimeElapsed += timeElapsed;
            if (TimeElapsed > Delay)
            {
                // Remove Teleporter from the current cell
                ((LevelScene)GameController.CurrentScene).GetCell(Top, Left).LeaveCell(this);

                PlaceCharacter();

                // Reset TimeElapsed
                TimeElapsed = TimeSpan.FromSeconds(0);
            }
        }
    }
}
