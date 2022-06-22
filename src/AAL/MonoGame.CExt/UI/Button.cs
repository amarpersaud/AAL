using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
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
                if (_text != null)
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

        public Color PressedColor;
        public Sprite PressedTexture;

        public Button(Sprite BaseTexture, Sprite PressedTexture) : base()
        {
            this.BackgroundSprite = BaseTexture;
            this.PressedTexture = PressedTexture;

            PressedColor = Color.Green;
            BackgroundColor = Color.Gray;
        }

        public override void Update(GameTime gameTime, double timeScale, InputHelper ih, UIHandler uih)
        {
            base.Update(gameTime, timeScale, ih, uih);

            if (Enabled)
            {

                //Children should be null, but update them if there are
                if (Children != null)
                {
                    foreach (var c in Children)
                    {
                        c.Update(gameTime, timeScale, ih, uih);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            if (Visible)
            {
                if (Hover & !Pressed)
                {
                    sb.Draw(this.BackgroundSprite.BaseTexture, this.ScreenBounds, BackgroundColor);
                }
                else
                {
                    sb.Draw(this.PressedTexture.BaseTexture, this.ScreenBounds, PressedColor);
                }


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
