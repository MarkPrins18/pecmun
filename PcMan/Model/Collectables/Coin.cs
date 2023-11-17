using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Collectables
{
    internal class Coin : IViewable, ICollectable
    {
        public int Top;
        public int Left;

        public Coin()
        {
            PlaceCollectable();
        }

        public Coin(int top, int left)
        {
            Top = top;
            Left = left;

            ((LevelScene)GameController.CurrentScene).GetCell(Top, Left).EnterCell(this);
        }

        public ConsoleColor GetColor()
        {
            return ConsoleColor.Yellow;
        }

        public string GetImage()
        {
            return "$";
        }

        public int GetLeft()
        {
            return Left;
        }

        public int GetTop()
        {
            return Top;
        }

        public void Pickup()
        {
            ((LevelScene)GameController.CurrentScene).AddScore();
            ((LevelScene)GameController.CurrentScene).GetCell(Top, Left).LeaveCell(this);

            PlaceCollectable();
        }
        public void PlaceCollectable()
        {
            bool placed = false;
            while (!placed)
            {
                // Choose random Top, between 1 and Height - 1
                int newTop = GameController.CurrentGame.RandomBetween(1, GameController.CurrentGame.GetHeight() - 1);

                // Choose random Left, between 1 and Width - 1
                int newLeft = GameController.CurrentGame.RandomBetween(1, GameController.CurrentGame.GetWidth() - 1);

                // Check if this cell exists and if it is enterable
                if (((LevelScene)GameController.CurrentScene).GetCell(newTop, newLeft) != null &&
                    ((LevelScene)GameController.CurrentScene).GetCell(newTop, newLeft).CanEnter())
                {
                    Top = newTop;
                    Left = newLeft;
                    ((LevelScene)GameController.CurrentScene).GetCell(Top, Left).EnterCell(this);

                    placed = true;
                }
            }
        }

        public void Remove()
        {
            ((LevelScene)GameController.CurrentScene).GetCell(Top, Left).LeaveCell(this);
        }
    }
}
