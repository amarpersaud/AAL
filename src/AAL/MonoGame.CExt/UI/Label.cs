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
    /// <summary>
    /// Label UI element for text labels
    /// </summary>
    public class Label : UIControl
    {
        /// <summary>
        /// Text to display on the label
        /// </summary>
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
        /// If true, centers the text on the given position.
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

        /// <summary>
        /// If text in label can be selected
        /// </summary>
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

        /// <summary>
        /// Create a text label
        /// </summary>
        /// <param name="resourceHandler">ResourceHandler object for obtaining loaded textures, fonts, and text.</param>
        public Label(ResourceHandler resourceHandler) : base(resourceHandler)
        {

        }

        /// <summary>
        /// Draw label element
        /// </summary>
        public override void Draw()
        {
            //Don't draw label if not visible
            if (!Visible)
            {
                return;
            }

            //Draw underlying UIControl 
            base.Draw();

            
            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = sb.GraphicsDevice.ScissorRectangle;

            //Cut off text if label hides overflow
            if (Overflow == UIOverflow.Hidden)
            {
                //Set the current scissor rectangle
                sb.GraphicsDevice.ScissorRectangle = this.ScreenInnerRect;
            }

            //If label has text and a font
            if (Text != null && Font != null)
            {
                //Draw it to the screen
                sb.DrawString(Font, Text, TextPosition.ToVector2(), ForeColor);
            }

            //Restore scissor rectangle 
            sb.GraphicsDevice.ScissorRectangle = currentRect;
        }

    }
}
