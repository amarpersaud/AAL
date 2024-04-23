using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.CExt.Sprites;
using Microsoft.Xna.Framework;
namespace MonoGame.CExt.UI
{

    /// <summary>
    /// Helper for converting converting between coordinates and grid spaces.
    /// </summary>
    public class GridHelper
    {
        public Point Start;
        public Point ItemSize;
        public Borders Margin;
        public bool spacingEnabled;
        public Point Spacing;
        public GridHelper(Point start, Point itemSize, Borders margin)
        {
            Start = start;
            ItemSize = itemSize;
            Margin = margin;
            this.spacingEnabled = false;
        }
        public GridHelper(Point start, Point itemSize, Point spacing)
        {
            Start = start;
            ItemSize = itemSize;
            this.Spacing = spacing;
            this.spacingEnabled = true;
        }

        public Rectangle GetRect(int x, int y)
        {
            Rectangle r = new Rectangle();
            r.Width = ItemSize.X;
            r.Height = ItemSize.Y;

            if (spacingEnabled)
            {
                r.X = x * (ItemSize.X + Spacing.X);
                r.Y = y * (ItemSize.Y + Spacing.Y);
            }
            else
            {
                r.X = x * (ItemSize.X + Margin.Right) + (x + 1) * Margin.Left;
                r.X = y * (ItemSize.Y + Margin.Bottom) + (y + 1) * Margin.Top;
            }

            return r;
        }

    }
}
