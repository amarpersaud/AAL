using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.CExt.Extensions;
using MonoGame.CExt.Input;
using MonoGame.CExt.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    /// <summary>
    /// Panel UI control. Basic element for holding other elements. Implements the basic scrolling
    /// </summary>
    public class Panel : UIControl
    {

        #region Scrolling

        /// <summary>
        /// True if X scroll bar is enabled. Only possible for overflow auto or scroll.
        /// </summary>
        public bool ScrollXEnabled => ((this.Overflow == UIOverflow.Auto) && (this.InnerRect.Width < this.GetContentBounds().Width)) || this.Overflow == UIOverflow.Scroll;

        /// <summary>
        /// True if Y scroll bar is enabled. Only possible for overflow auto or scroll.
        /// </summary>
        public bool ScrollYEnabled => ((this.Overflow == UIOverflow.Auto) && (this.InnerRect.Height < this.GetContentBounds().Height)) || this.Overflow == UIOverflow.Scroll;

        /// <summary>
        /// Maximum scrolling amount
        /// </summary>
        public Point MaxChildOffset
        {
            get
            {
                //Get bounds of content
                Rectangle ContentsScreenRect = GetContentBounds();
                
                //Return difference so that bottom of content will match to bottom of the inner rect
                return ContentsScreenRect.Size - this.InnerRect.Size;
            }
        }

        #endregion Scrolling


        /// <summary>
        /// UI Panel that holds other controls
        /// </summary>
        public Panel(ResourceHandler resourceHandler) : base(resourceHandler) {
            
        }

        /// <summary>
        /// Update the UI Panel and its children. TODO: add UpdateChildren function and call at end of Update to avoid base.update from updating child elements before main element
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="timeScale"></param>
        /// <param name="uih"></param>
        public override void Update(GameTime gameTime, double timeScale, UIHandler uih)
        {
            if (!Enabled)
            {
                return;
            }

            base.Update(gameTime, timeScale, uih);

            float ScrollVelocityScale = 0.1f;

            //This is the selected control
            if(uih.SelectedControl == this)
            {
                //determine if scrolling has occurred regardless of mouse position.
                Point r = MaxChildOffset;

                //Vertical scrolling
                if (ScrollYEnabled)
                {
                    //Scrolling down -> move contents up
                    if(ih.MouseScrollWheelVelocityY < 0)
                    {
                        //Decrease child offset by scrollwheel velocity and clamp
                        ChildOffsetY = MathExt.Clamp(ChildOffsetY + (int)(ih.MouseScrollWheelVelocityY * ScrollVelocityScale), -(MaxChildOffset.Y), 0);
                    }
                    //Scrolling up -> move contents down
                    else if (ih.MouseScrollWheelVelocityY > 0)
                    {
                        //Increase child offset 
                        ChildOffsetY = MathExt.Clamp(ChildOffsetY + (int)(ih.MouseScrollWheelVelocityY * ScrollVelocityScale), -(MaxChildOffset.Y), 0);
                    }
                }
                //Horizontal scrolling
                if(ScrollXEnabled && ih.MouseScrollWheelVelocityX != 0)
                {
                    //Scrolling right -> move contents left
                    if (ih.MouseScrollWheelVelocityX < 0)
                    {
                        //Decrease child offset by scrollwheel velocity and clamp
                        ChildOffsetX = MathExt.Clamp(ChildOffsetX - (int)(ih.MouseScrollWheelVelocityX), -MaxChildOffset.X, 0);
                    }
                    //Scrolling left -> move contents right
                    else if (ih.MouseScrollWheelVelocityX > 0)
                    {
                        //Increase child offset 
                        ChildOffsetX = MathExt.Clamp(ChildOffsetX + (int)(ih.MouseScrollWheelVelocityX), -MaxChildOffset.X, 0);
                    }
                }
            
            }

            if(Children != null)
            {
                foreach(var c in Children)
                {
                    c.Update(gameTime, timeScale, uih);
                }
            }
        }

        public override void Draw()
        {
            if (!Visible)
            {
                return;
            }

            base.Draw();

            //Draw panel background
            sb.Draw(this.BackgroundSprite.BaseTexture, this.ScreenBounds, this.BackgroundColor);

            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = sb.GraphicsDevice.ScissorRectangle;

            if (Overflow != UIOverflow.Visible)
            {
                //Set the current scissor rectangle
                sb.GraphicsDevice.ScissorRectangle = this.ScreenInnerRect;
            }


            foreach (var c in Children)
            {
                c.Draw();
            }

            sb.GraphicsDevice.ScissorRectangle = currentRect;

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
        /// <param name="offset">If true, adds parent offset to controls</param>
        /// <returns>Rectangle with bounds of contents in screen coordinates, and offset by childoffset if offset is true.</returns>
        public Rectangle GetContentScreenBounds(bool offset)
        {
            var r = GetContentBounds();
            r.Location += this.ScreenLocation;
            if (offset)
            {
                r.X += ChildOffsetX;
                r.Y += ChildOffsetY;
            }
            return r;
        }
    }
}

