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
    public class Button : UIControl
    {
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                if (_text != null && Font != null)
                {
                    TextDimensions = this.Font.MeasureString(Text);
                }
            }
        }

        private Vector2 TextDimensions;

        public Point TextPosition
        {
            get
            {
                return (this.ScreenLocation.ToVector2() + (this.Size.ToVector2() / 2.0f) - (TextDimensions / 2.0f)).ToPoint();
            }
        }

        private string _text;

        public Color HoverColor;
        public Sprite HoverTexture;

        public Button(ResourceHandler resourceHandler) : base(resourceHandler)
        {
            this.BackgroundSprite = _rh.GetSprite("whiteRect");
            this.HoverTexture = _rh.GetSprite("whiteRect");
            this.FontName = "Arial";

            HoverColor = Color.Green;
            BackgroundColor = Color.Gray;
        }

        public override void Update(GameTime gameTime, double timeScale, UIHandler uih)
        {
            if (!Enabled)
            {
                return;
            }

            base.Update(gameTime, timeScale, uih);

            //Children should be null, but update them if there are
            if (Children != null)
                {
                    foreach (var c in Children)
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

            if (Hover & !Pressed)
            {
                sb.Draw(this.HoverTexture.BaseTexture, this.ScreenBounds, HoverColor);
            }
            else
            {
                sb.Draw(this.BackgroundSprite.BaseTexture, this.ScreenBounds, BackgroundColor);
            }


            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = sb.GraphicsDevice.ScissorRectangle;

            if (Overflow != UIOverflow.Visible)
            {
                //Set the current scissor rectangle
                sb.GraphicsDevice.ScissorRectangle = this.ScreenInnerRect;
            }

            if (Text != null && Font != null)
            {
                sb.DrawString(Font, Text, TextPosition.ToVector2(), ForeColor);
            }

            //Children should be null, but draw them if there are
            if (Children != null)
            {
                foreach (var c in Children)
                {
                    c.Draw();
                }
            }

            //Restore scissor rectangle
            sb.GraphicsDevice.ScissorRectangle = currentRect;
        }
    }
}
