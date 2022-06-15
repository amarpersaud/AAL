using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    /// <summary>
    /// Overflow type for content of UI Control.
    /// </summary>
    public enum UIOverflow
    {
        /// <summary>
        /// Overflow always visible
        /// </summary>
        Visible,

        /// <summary>
        /// Overflow cropped to inner rectangle bounds
        /// </summary>
        Hidden,

        /// <summary>
        /// Overflow allows scrolling in both directions
        /// </summary>
        Scroll,

        /// <summary>
        /// Overflow allows scrolling. Bars only shown if there is overflow.
        /// </summary>
        Auto
    }
}
