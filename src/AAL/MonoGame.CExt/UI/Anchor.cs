using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    public struct Anchor
    {
        /// <summary>
        /// If left is anchored
        /// </summary>
        public bool Top;

        /// <summary>
        /// If right is anchored
        /// </summary>
        public bool Right;

        /// <summary>
        /// If bottom side is anchored
        /// </summary>
        public bool Bottom;

        /// <summary>
        /// If left side is anchored
        /// </summary>
        public bool Left;

        /// <summary>
        /// Default UI control anchors. Top and left side are anchored.
        /// </summary>
        public static Anchor Default = new Anchor { Top = true, Bottom = false, Right = false, Left = true };

        /// <summary>
        /// No anchors. Identical to new Anchor struct
        /// </summary>
        public static Anchor None = new Anchor{ Top = false, Bottom = false, Right = false, Left = false };

        /// <summary>
        /// All sides anchored.
        /// </summary>
        public static Anchor All = new Anchor { Top = true, Bottom = true, Right = true, Left = true };
    }
}
