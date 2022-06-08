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
        /// Distance of Top edge from parent's top edge.
        /// </summary>
        public int TopDistance;

        /// <summary>
        /// If right is anchored
        /// </summary>
        public bool Right;

        /// <summary>
        /// Distance of Right edge from parent's top edge.
        /// </summary>
        public int RightDistance;

        /// <summary>
        /// If bottom side is anchored
        /// </summary>
        public bool Bottom;

        /// <summary>
        /// Distance of Bottom edge from parent's top edge.
        /// </summary>
        public int BottomDistance;

        /// <summary>
        /// If left side is anchored
        /// </summary>
        public bool Left;

        /// <summary>
        /// Distance of Left edge from parent's top edge.
        /// </summary>
        public int LeftDistance;

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
