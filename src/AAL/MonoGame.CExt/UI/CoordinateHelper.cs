using Microsoft.Xna.Framework;
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
        /// Start X position
        /// </summary>
        public int X { get; set; } = 0;

        /// <summary>
        /// Start Y position
        /// </summary>
        public int Y { get; set; } = 0;

        /// <summary>
        /// Integer width of parent object
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Integer height of parent object
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Bounds of coordinate helper object
        /// </summary>
        public Rectangle Bounds { get; set; }


        /// <summary>
        /// Create coordinate helper object
        /// </summary>
        /// <param name="Width">Width of parent</param>
        /// <param name="Height">Height of parent</param>
        public CoordinateHelper(int Width, int Height)
        {
            X = 0;
            Y = 0;
            this.Width = Width;
            this.Height = Height;
        }

        /// <summary>
        /// Create coordinate helper object
        /// </summary>
        /// <param name="X">Start X</param>
        /// <param name="Y">Start Y</param>
        /// <param name="Width">Width of parent</param>
        /// <param name="Height">Height of parent</param>
        public CoordinateHelper(int X, int Y, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }

        /// <summary>
        /// Create coordinate helper object
        /// </summary>
        /// <param name="rect">Rectangle with dimensions</param>
        public CoordinateHelper(Rectangle rect)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }


        /// <summary>
        /// Convert percentage to horizontal position
        /// </summary>
        /// <param name="p">Percentage of parent width</param>
        /// <returns>integer percentage p of width</returns>
        public int atoiX(double p) => (int)(p * Width) + X;

        /// <summary>
        /// Convert percentage to vertical position
        /// </summary>
        /// <param name="p">Percentage of parent height</param>
        /// <returns>integer percentage p of height</returns>
        public int atoiY(double p) => (int)(p * Height) + Y;

        /// <summary>
        /// Converts horizontal position to percentage of parent width
        /// </summary>
        /// <param name="x">Horizontal position</param>
        /// <returns>percentage of point to parent width</returns>
        public double itoaX(int x) => ((double)(x-this.X)) / Width;

        /// <summary>
        /// Converts vertical position to percentage of parent height
        /// </summary>
        /// <param name="x">Vertical position</param>
        /// <returns>percentage of point to parent height</returns>
        public double itoaY(int y) => ((double)(y-this.Y)) / Height;
    }
}
