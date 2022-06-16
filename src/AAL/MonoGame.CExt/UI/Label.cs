using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    public class Label : UIControl
    {
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                if (_text != null)
                {
                    TextDimensions = this.Font.MeasureString(Text);
                }
            }
        }

        /// <summary>
        /// Dimensions of text with given font
        /// </summary>
        private Vector2 TextDimensions;

        /// <summary>
        /// Centers the text on the given position
        /// </summary>
        public bool Centered { get; set; } = false;

        /// <summary>
        /// Position on screen of top left corner of text
        /// </summary>
        public Point TextPosition
        {
            get
            {
                if (Centered)
                {
                    return (this.ScreenLocation.ToVector2() + (this.Size.ToVector2() / 2.0f) - (TextDimensions / 2.0f)).ToPoint();
                }
                return this.ScreenLocation;
            }
        }

        private string _text;

        public Label() : base()
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            if (Visible)
            {
                //Copy the current scissor rect so we can restore it after
                Rectangle currentRect = sb.GraphicsDevice.ScissorRectangle;

                if (Overflow == UIOverflow.Hidden)
                {
                    //Set the current scissor rectangle
                    sb.GraphicsDevice.ScissorRectangle = this.ScreenInnerRect;
                }

                if (Text != null && Font != null)
                {
                    sb.DrawString(Font, Text, TextPosition.ToVector2(), ForeColor);
                }

                //Restore scissor rectangle
                sb.GraphicsDevice.ScissorRectangle = currentRect;
            }
        }


    }
}
