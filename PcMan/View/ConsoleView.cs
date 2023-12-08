using PcMan.Model;
using PcMan.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.View
{
    /// <summary>
    /// ConsoleView class handles the display of game elements and messages in a console window.
    /// </summary>
    public class ConsoleView
    {
        int Width;
        int Height;
        int Side = 50;

        /// <summary>
        /// Initializes a new instance of the ConsoleView class with the specified height and width.
        /// </summary>
        /// <param name="height">The height of the console window.</param>
        /// <param name="width">The width of the console window.</param>
        public ConsoleView(int height, int width)
        {

            Width = width;
            Height = height;

            ClearScreen();
        }

        /// <summary>
        /// Clears the console screen and sets up the initial display.
        /// </summary>
        public void ClearScreen()
        {
            Console.SetWindowSize(Width + Side, Height);

            // Set color to white
            Console.ForegroundColor = ConsoleColor.White;

            // ClearWalls the entire screen
            Console.Clear();

            CreateBoundary();
        }

        /// <summary>
        /// Draws a viewable object in the console window.
        /// </summary>
        /// <param name="viewable">The IViewable object to be drawn.</param>
        public void Draw(IViewable viewable)
        {
            Console.SetCursorPosition(viewable.GetLeft(), viewable.GetTop());
            Console.ForegroundColor = viewable.GetColor();
            Console.Write(viewable.GetImage());
        }

        /// <summary>
        /// Shows a message in the center of the console window.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        public void ShowMessage(string message)
        {
            // Display the message in the center of the screen
            Console.SetCursorPosition(Width / 2 - message.Length / 2, Height / 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
        }

        /// <summary>
        /// Displays a message on screen
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="top">Position from the top</param>
        /// <param name="allignment">Left, center or right allignment(0, 1 or 3)</param>
        /// <param name="color">Color of the text</param>
        internal void Show(string message, int top, int allignment, ConsoleColor color)
        {
            // Calculate the horizontal position of the message, based on it's length and allignment
            int left = 0;
            switch (allignment)
            {
                case 0:
                    left = 1;
                    break;
                case 1:
                    left = Width / 2 - message.Length / 2;
                    break;
                case 2:
                    left = Width - message.Length - 1;
                    break;
            }

            // Display the message
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = color;
            Console.Write(message);
        }

        /// <summary>
        /// Displays a left-aligned message on the console window with a specified position and color.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="top">Position from the top.</param>
        /// <param name="color">Color of the text.</param>
        internal void Left(string message, int top, ConsoleColor color)
        {
            Show(message, top, 0, color);
        }

        /// <summary>
        /// Displays a centered message on the console window with a specified position and color.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="top">Position from the top.</param>
        /// <param name="color">Color of the text.</param>
        internal void Center(string message, int top, ConsoleColor color)
        {
            Show(message, top, 1, color);
        }

        /// <summary>
        /// Displays a right-aligned message on the console window with a specified position and color
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="top">Position from the top.</param>
        /// <param name="color">Color of the text.</param>
        internal void Right(string message, int top, ConsoleColor color)
        {
            Show(message, top, 2, color);
        }

        /// <summary>
        /// Creates a boundary around the play area in the console window.
        /// </summary>
        private void CreateBoundary()
        {
            // Create a boundary around the play area using single-line border
            for (int i = 0; i < Width; i++)
            {
                if (i == 0)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("┌");
                    Console.SetCursorPosition(i, Height - 1);
                    Console.Write("└");
                }
                else if (i == Width - 1)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("┐");
                    Console.SetCursorPosition(i, Height - 1);
                    Console.Write("┘");
                }
                else
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("─");
                    Console.SetCursorPosition(i, Height - 1);
                    Console.Write("─");
                }
            }
            for (int i = 1; i < Height - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│");
                Console.SetCursorPosition(Width - 1, i);
                Console.Write("│");
            }
        }

        /// <summary>
        /// [DEPRECATED] This method is deprecated. Use Show instead. 
        /// Writes text at a specified position in the console window with a specified color.
        /// </summary>
        /// <param name="top">The top position of the text.</param>
        /// <param name="left">The left position of the text.</param>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="color">The color of the text.</param>
        public void Write(int top, int left, string text, ConsoleColor color)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = color;
            Console.Write(text);
        }
    }
}
