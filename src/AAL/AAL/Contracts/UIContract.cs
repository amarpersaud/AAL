using System;
using System.Collections.Generic;
using System.Text;

using MonoGame.CExt;
using MonoGame.CExt.UI;
using MonoGame.CExt.Utility;
using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
using MonoGame.CExt.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAL.Contracts
{
    /// <summary>
    /// A UI Control for physically displaying the contract on screen
    /// </summary>
    public class UIContract : UIControl
    {
        /// <summary>
        /// Contract object that holds contract information
        /// </summary>
        public Contract BaseContract; 

        public UIContract() : base()
        {

        }

        public override void Update(GameTime gameTime, double timeScale, InputHelper ih, UIHandler uih)
        {
            base.Update(gameTime, timeScale, ih, uih);

        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            if (Visible)
            {
                //Draw panel background
                sb.Draw(this.BackgroundSprite.BaseTexture, this.ScreenBounds, this.BackgroundColor);


                //Copy the current scissor rect so we can restore it after
                Rectangle currentRect = sb.GraphicsDevice.ScissorRectangle;

                if (Overflow == UIOverflow.Hidden)
                {
                    //Set the current scissor rectangle
                    sb.GraphicsDevice.ScissorRectangle = this.ScreenInnerRect;
                }

                //Draw title
                //Draw text

                /*
                if (Text != null && Font != null)
                {
                    sb.DrawString(Font, Text, TextPosition.ToVector2(), ForeColor);
                }
                */


                //Children should be null, but draw them if there are
                if (Children != null)
                {
                    foreach (var c in Children)
                    {
                        c.Draw(sb);
                    }
                }

                //Restore scissor rectangle
                sb.GraphicsDevice.ScissorRectangle = currentRect;
            }
        }
    }
}
