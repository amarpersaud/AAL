using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    public enum Position
    {
        /// <summary>
        /// Default positioning in internal rectangle. Cannot overlap padding.
        /// </summary>
        Default,
        /// <summary>
        /// Absolute position relative to viewport
        /// </summary>
        Absolute,
        /// <summary>
        /// Positioned relative to parent. Similar to default but ignores padding.
        /// </summary>
        Relative,
        /// <summary>
        /// Inherit positioning from parent. Rarely used.
        /// </summary>
        Inherit
    }
}
