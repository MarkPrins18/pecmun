using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Characters
{
    public class Character : IViewable
    {
        // Public positions
        public int Left;
        public int Top;

        // public timerts
        public TimeSpan Delay;
        public TimeSpan TimeElapsed;

        // Characters symbol and color
        public string Symbol;
        public ConsoleColor Color;

        public virtual bool TryMove(int deltaTop, int deltaLeft)
        {
            if (CanMove(deltaTop, deltaLeft))
            {
                Move(deltaTop, deltaLeft);
                return true;
            }
            return false;
        }

        public virtual bool CanMove(int deltaTop, int deltaLeft)
        {
            // Check if new position is valid
            if (
                    Top + deltaTop < 0 ||
                    Top + deltaTop > GameController.CurrentGame.GetHeight() - 1 ||
                    Left + deltaLeft < 0 ||
                    Left + deltaLeft > GameController.CurrentGame.GetWidth() - 1)
            {
                return false;
            }

            // Check if there is a cell in the Game cell-array
            if (((LevelScene)GameController.CurrentScene).Cells[Top + deltaTop, Left + deltaLeft] != null)
            {
                // Check if the cell is enterable
                if (((LevelScene)GameController.CurrentScene).Cells[Top + deltaTop, Left + deltaLeft].CanEnter())
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void Move(int deltaTop, int deltaLeft)
        {
            // Leave the current cell
            ((LevelScene)GameController.CurrentScene).Cells[Top, Left].LeaveCell(this);

            // Set new position
            Top += deltaTop;
            Left += deltaLeft;

            // Enter the new cell
            ((LevelScene)GameController.CurrentScene).Cells[Top, Left].EnterCell(this);
        }

        public int GetTop()
        {
            return Top;
        }



        public virtual string GetImage()
        {
            return Symbol;
        }

        public ConsoleColor GetColor()
        {
            return Color;
        }

        public void PlaceCharacter()
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
                    Move(0, 0);

                    placed = true;
                }
            }
        }

        public void Die()
        {
            ((LevelScene)GameController.CurrentScene).GetCell(Top, Left).LeaveCell(this);
        }

        public int GetLeft()
        {
            throw new NotImplementedException();
        }
    }
}
