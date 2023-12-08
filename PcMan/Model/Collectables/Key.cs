using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Collectables
{
    public class Key : IViewable, ICollectable
    {
        private int top;
        private int left;

        private Wall wall;

        public Key (int top, int left, Wall wall)
        {
            this.top = top;
            this.left = left;

            // check if wall is null
            if (wall == null)
            {
                throw new ArgumentNullException("Wall cannot be null");
            }

            this.wall = wall;

            // register the key with the cell
            ((LevelScene)GameController.CurrentScene).Cells[top, left].EnterCell(this);
        }

        public ConsoleColor GetColor()
        {
            return ConsoleColor.Yellow;
        }

        public string GetImage()
        {
            return "þ";
        }

        public int GetLeft()
        {
            return left;
        }

        public int GetTop()
        {
            return top;
        }

        public void Pickup()
        {
            // If the key is picked up, it should
            // "open" the wall connected to it.
            ((LevelScene)GameController.CurrentScene).GetCell(wall.Top, wall.Left).MakeEnterable();
            ((LevelScene)GameController.CurrentScene).GetCell(top, left).LeaveCell(this);

        }

        public void Remove()
        {
            ((LevelScene)GameController.CurrentScene).GetCell(top, left).LeaveCell(this);
        }
    }
}
