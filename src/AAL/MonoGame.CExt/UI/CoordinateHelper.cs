using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    /// <summary>
    /// Coordinate helper. Helps conversion between percentage and int coordinates
    /// </summary>
    public class CoordinateHelper
    {
        /// <summary>
        /// Integer width of parent object
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Integer height of parent object
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Create coordinate helper object
        /// </summary>
        /// <param name="Width">Width of parent</param>
        /// <param name="Height">Height of parent</param>
        public CoordinateHelper(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        /// <summary>
        /// Convert percentage to horizontal position
        /// </summary>
        /// <param name="p">Percentage of parent width</param>
        /// <returns>integer percentage p of width</returns>
        public int atoiX(double p) => (int)(p * Width);

        /// <summary>
        /// Convert percentage to vertical position
        /// </summary>
        /// <param name="p">Percentage of parent height</param>
        /// <returns>integer percentage p of height</returns>
        public int atoiY(double p) => (int)(p * Height);

        /// <summary>
        /// Converts horizontal position to percentage of parent width
        /// </summary>
        /// <param name="x">Horizontal position</param>
        /// <returns>percentage of point to parent width</returns>
        public double itoaX(int x) => ((double)x) / Width;

        /// <summary>
        /// Converts vertical position to percentage of parent height
        /// </summary>
        /// <param name="x">Vertical position</param>
        /// <returns>percentage of point to parent height</returns>
        public double itoaY(int y) => ((double)y) / Height;
    }
}
