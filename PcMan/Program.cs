using PcMan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan
{
    /// <summary>
    /// The Program class
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main method
        /// </summary>
        /// <param name="args"></param>
        static public void Main(String[] args)
        {
            // Set cursor to invisible
            Console.CursorVisible = false;
            
            // Create a new gameController
            // Game gameController = new Game();
            GameController gameController = new GameController();
            gameController.Play();

            // Clear the entire console and set cursor to visible
            Console.Clear();
            Console.CursorVisible = true;
        }
    }
}