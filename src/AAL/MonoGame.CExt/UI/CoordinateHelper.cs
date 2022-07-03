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
        /// Ignore the X and Y offset
        /// </summary>
        public bool IgnoreOffset { get; set; } = false;

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
        public CoordinateHelper(Rectangle rect, bool ignoreOffset = true)
        {
            this.X = rect.X;
            this.Y = rect.Y;
            this.Width = rect.Width;
            this.Height = rect.Height;
            this.IgnoreOffset = ignoreOffset;
        }


        /// <summary>
        /// Convert percentage to horizontal position
        /// </summary>
        /// <param name="p">Percentage of parent width</param>
        /// <returns>integer percentage p of width</returns>
        public int atoiX(double p)
        {
            if (IgnoreOffset)
            {
                return (int)(p * Width);
            }
            return (int)(p * Width) + X;
        }


        /// <summary>
        /// Convert percentage to vertical position
        /// </summary>
        /// <param name="p">Percentage of parent height</param>
        /// <returns>integer percentage p of height</returns>
        public int atoiY(double p)
        {
            if (IgnoreOffset)
            {
                return (int)(p * Height);
            }
            return (int)(p * Height) + Y;
        }
        /// <summary>
        /// Converts horizontal position to percentage of parent width
        /// </summary>
        /// <param name="x">Horizontal position</param>
        /// <returns>percentage of point to parent width</returns>
        public double itoaX(int x)
        {
            if (IgnoreOffset)
            {
                return (double)x / Width;
            }
            return ((double)(x-this.X)) / Width;
        }

        /// <summary>
        /// Converts vertical position to percentage of parent height
        /// </summary>
        /// <param name="x">Vertical position</param>
        /// <returns>percentage of point to parent height</returns>
        public double itoaY(int y)
        {
            if (IgnoreOffset)
            {
                return (double)y / Height;
            }
            return ((double)(y - this.Y)) / Height;
        }

        /// <summary>
        /// Create rectangle from percentage of parent dimensions
        /// </summary>
        /// <param name="x">X percentage of parent width</param>
        /// <param name="y">Y percentage of parent width</param>
        /// <param name="width">Width percentage of parent width</param>
        /// <param name="height">Height percentage of parent width</param>
        /// <returns>Rectangle with dimensions</returns>
        public Rectangle GetRect(double x, double y, double width, double height)
        {
            return new Rectangle(atoiX(x), atoiY(y), atoiX(width), atoiY(height));
        }

        /// <summary>
        /// Create rectangle from percentage of parent dimensions
        /// </summary>
        /// <param name="x">X percentage of parent width</param>
        /// <param name="y">Y percentage of parent width</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>Rectangle with dimensions</returns>
        public Rectangle GetRect(double x, double y, int width, int height)
        {
            return new Rectangle(atoiX(x), atoiY(y), width, height);
        }

        /// <summary>
        /// Get rectangle
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="width">Width percentage of parent width</param>
        /// <param name="height">Height percentage of parent width</param>
        /// <returns>Rectangle centered on position with percent dimensions</returns>
        public Rectangle GetRect(int x, int y, double width, double height)
        {
            return new Rectangle(x, y, atoiX(width), atoiY(height));
        }

        /// <summary>
        /// Get rectangle centered on a position
        /// </summary>
        /// <param name="x">X percentage of parent width</param>
        /// <param name="y">Y percentage of parent width</param>
        /// <param name="width">Width percentage of parent width</param>
        /// <param name="height">Height percentage of parent width</param>
        /// <returns>Rectangle centered on position with percent dimensions</returns>
        public Rectangle GetRectCentered(double x, double y, double width, double height)
        {
            int aw = atoiX(width);
            int ah = atoiY(height);
            return new Rectangle(atoiX(x) - aw/2, atoiY(y) - ah/2, aw, ah);
        }

        /// <summary>
        /// Get rectangle centered on a position
        /// </summary>
        /// <param name="x">X percentage of parent width</param>
        /// <param name="y">Y percentage of parent width</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>Rectangle centered on position with percent dimensions</returns>
        public Rectangle GetRectCentered(double x, double y, int width, int height)
        {
            return new Rectangle(atoiX(x) - width / 2, atoiY(y) - height / 2, width, height);
        }


        /// <summary>
        /// Get rectangle centered on a position
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="width">Width percentage of parent width</param>
        /// <param name="height">Height percentage of parent width</param>
        /// <returns>Rectangle centered on position with percent dimensions</returns>
        public Rectangle GetRectCentered(int x, int y, double width, double height)
        {
            return new Rectangle(x - atoiX(width) / 2, y - atoiY(height) / 2, atoiX(width), atoiY(height));
        }
    }
}
