using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model
{
    /// <summary>
    /// Represents a direction by storing the change in the top and left position.
    /// </summary>
    internal class Direction
    {
        int deltaTop;
        int deltaLeft;

        /// <summary>
        /// Initializes a new instance of the Direction class with the specified deltaTop and deltaLeft values.
        /// </summary>
        /// <param name="deltaTop">The change in the top position.</param>
        /// <param name="deltaLeft">The change in the left position.</param>
        public Direction(int deltaTop, int deltaLeft)
        {
            this.deltaTop = deltaTop;
            this.deltaLeft = deltaLeft;
        }

        /// <summary>
        /// Initializes a new instance of the Direction class with a random direction and the specified step size.
        /// </summary>
        /// <param name="stepSize">The step size to use for the randomly generated direction.</param>
        public Direction(int stepSize)
        {
            int rnd = new Random().Next(0, 4);
            if (rnd == 0)
            {
                deltaTop = stepSize;
                deltaLeft = 0;
            }
            else if (rnd == 1)
            {
                deltaTop = -stepSize;
                deltaLeft = 0;
            }
            else if (rnd == 2)
            {
                deltaTop = 0;
                deltaLeft = stepSize;
            }
            else if (rnd == 3)
            {
                deltaTop = 0;
                deltaLeft = -stepSize;
            }
        }

        /// <summary>
        /// Returns the direction as an array containing deltaTop and deltaLeft.
        /// </summary>
        /// <returns>An array with deltaTop at index 0 and deltaLeft at index 1.</returns>
        public int[] getDirection()
        {
            int[] direction = new int[2];
            direction[0] = deltaTop;
            direction[1] = deltaLeft;

            return direction;

        }

        /// <summary>
        /// Determines if the direction is horizontal.
        /// </summary>
        /// <returns>Returns true if the direction is horizontal, otherwise false.</returns>
        public bool IsHorizontal()
        {
            return deltaLeft != 0;
        }

        /// <summary>
        /// Determines if the direction is vertical.
        /// </summary>
        /// <returns>Returns true if the direction is vertical, otherwise false.</returns>
        public bool IsVertical()
        {
            return deltaTop != 0;
        }
    }
}
