using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    public class Panel : UIControl
    {

        #region Scrolling

        /// <summary>
        /// True if X scroll bar is enabled. Only possible for overflow auto or scroll.
        /// </summary>
        public bool ScrollXEnabled => ((this.Overflow == UIOverflow.Auto) && (this.InnerRect.Width < this.GetContentBounds().Width)) || this.Overflow == UIOverflow.Scroll ;

        /// <summary>
        /// True if Y scroll bar is enabled. Only possible for overflow auto or scroll.
        /// </summary>
        public bool ScrollYEnabled => ((this.Overflow == UIOverflow.Auto) && (this.InnerRect.Height < this.GetContentBounds().Height)) || this.Overflow == UIOverflow.Scroll ;

        #endregion Scrolling


        public Panel() : base() {
            
        }


        /// <summary>
        /// Get bounds of content relative to the location of the control
        /// </summary>
        /// <returns>Rectangle with bounds of contents in local coordinates</returns>
        public Rectangle GetContentBounds()
        {
            Rectangle r = this.InnerRect;

            if(Children != null)
            {
                foreach(var c in Children)
                {
                    if(c.X < r.X)
                    {
                        r.X = c.X;
                    }
                    if (c.Y < r.Y)
                    {
                        r.Y = c.Y;
                    }

                    if (c.Right > r.Right)
                    {
                        r.Width = c.Right - r.X;
                    }

                    if (c.Bottom > r.Bottom)
                    {
                        r.Height = c.Bottom - r.Y;
                    }
                }
            }

            return r;
        }

        /// <summary>
        /// Get bounds of content on screen.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetContentScreenBounds()
        {
            var r = GetContentBounds();
            r.Location += this.ScreenLocation;
            return r;
        }
    }
}
