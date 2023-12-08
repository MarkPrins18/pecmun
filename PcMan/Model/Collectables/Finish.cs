using PcMan.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Collectables
{
    internal class Finish : IViewable, ICollectable
    {
        public int Top;
        public int Left;
        
        public Finish(int top, int left)
        {
            Top = top;
            Left = left;
        }
        public ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }

        public string GetImage()
        {
            return "ƒ";
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
            // For finish, this means end of level, so what to do? 
            throw new NotImplementedException();
        }

        public void Remove()
        {
            // For finish, this means end of level, so what to do? 
            throw new NotImplementedException();
        }
    }
}
