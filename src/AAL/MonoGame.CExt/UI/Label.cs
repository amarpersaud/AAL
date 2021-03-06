using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
using MonoGame.CExt.Utility;
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
                    Width = (int)TextDimensions.X;
                    Height = (int)TextDimensions.Y;
                }
            }
        }

        /// <summary>
        /// Dimensions of text with given font
        /// </summary>
        private Vector2 TextDimensions
        {
            get
            {
                if (Font == null || Text == null)
                {
                    return Vector2.Zero;
                }
                return Font.MeasureString(Text);
            }
        }

        /// <summary>
        /// Centers the text on the given position
        /// </summary>
        public bool Centered {
            get
            {
                return CenterX && CenterY;       
            }
            set {
                CenterX = value;
                CenterY = value;
            }
        }
        
        /// <summary>
        /// Centers the text horizontally, only
        /// </summary>
        public bool CenterX { get; set; }

        /// <summary>
        /// Centers the text vertically, only
        /// </summary>
        public bool CenterY { get; set; }

        public override bool Selectable => false;

        /// <summary>
        /// Position on screen of top left corner of text
        /// </summary>
        public Point TextPosition
        {
            get
            {
                int TPX = ScreenX;
                int TPY = ScreenY;


                if (CenterX)
                {
                    TPX = (int)((float)ScreenX - TextDimensions.X / 2.0f);
                }

                if (CenterY)
                {
                    TPY = (int)((float)ScreenY - TextDimensions.Y / 2.0f);
                }

                return new Point(TPX,TPY);
            }
        }

        private string _text;

        public Label(ResourceHandler resourceHandler) : base(resourceHandler)
        {

        }

        public override void Draw()
        {
            if (!Visible)
            {
                return;
            }

            base.Draw();

            
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
