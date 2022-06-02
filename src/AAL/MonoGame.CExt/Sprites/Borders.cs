using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.Sprites
{
    public struct Borders

    {
        /// <summary>
        /// Left border
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Top border
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Right border
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// Bottom border
        /// </summary>
        public int Bottom { get; set; }

        /// <summary>
        /// Borders struct with zero for all borders
        /// </summary>
        public static Borders Zero = new Borders { Left = 0, Right = 0, Top = 0, Bottom = 0 };
    }
}
