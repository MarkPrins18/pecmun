using PcMan.Model.Characters;
using PcMan.Model.Interfaces;
using PcMan.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model
{
    public class Cell : IViewable
    {
        private int top;
        private int left;

        private List<IViewable> viewables;
        private ConsoleView consoleView;

        private bool isEnterable;

        public Cell(int top, int left, ConsoleView consoleView)
        {
            viewables = new List<IViewable>();
            this.consoleView = consoleView;

            this.top = top;
            this.left = left;

            isEnterable = true;
        }

        public void EnterCell(IViewable viewable)
        {
            // If viewable is a player
            if(viewable is Player)
            {
                // for of all viewables in cell
                for (int i = 0; i < viewables.Count; i++) // Using a for-loop instead of a foreach loop, to prevent "Collection changed"-error
                {
                    // If viewable is an enemy
                    if (viewables[i] is ICanKill)
                    {
                        // The player has entered a cell with a IEnemy
                        // Cast viewable to a player
                        Player player = (Player)viewable;

                        // Die the player
                        player.Kill();
                    }

                    // if viewable is a collectable
                    if (viewables[i] is ICollectable)
                    {
                        ICollectable v = (ICollectable)viewables[i];

                        // remove collectable from cell
                        viewables.RemoveAt(i);

                        // pickup collectable
                        v.Pickup();

                        // decrement i
                        i--;
                    }
                }
            }

            // If viewable is a killer
            if (viewable is ICanKill)
            {
                // Check if there is a player in this cell
                foreach (IViewable v in viewables)
                {
                    if (v is Player)
                    {
                        // The bouncer has entered a cell with a player
                        // Cast levelIndex to a player
                        Player player = (Player)v;

                        // Die the player
                        player.Kill();
                    }
                }
            }

            viewables.Add(viewable);
            consoleView.Draw(this);
        }

        public void LeaveCell(IViewable viewable)
        {
            viewables.Remove(viewable);
            consoleView.Draw(this);
        }

        public bool CanEnter()
        {
            return isEnterable;
        }

        public int GetTop()
        {
            return top;
        }

        public int GetLeft()
        {
            return left;
        }

        public string GetImage()
        {
            if (viewables.Count == 0)
            {
                // If we are not enterable, return a wall
                if (!isEnterable)
                {
                    return "█";
                }
                return " ";
            }
            else
            {
                return viewables[viewables.Count - 1].GetImage();
            }
        }

        public ConsoleColor GetColor()
        {
            if (viewables.Count == 0)
            {
                return ConsoleColor.White;
            }
            else
            {
                return viewables[viewables.Count - 1].GetColor();
            }
        }

        public void MakeWall()
        {
            isEnterable = false;
            consoleView.Draw(this);
        }

        public void MakeEnterable()
        {
            isEnterable = true;
            consoleView.Draw(this);
        }

        public override string ToString()
        {
            return "Cell: " + top + ", " + left;
        }
    }
}
