using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.CExt.Sprites;
using MonoGame.CExt.Input;
using MonoGame.CExt.Extensions;

namespace MonoGame.CExt.UI
{
    public abstract class UIControl
    {
        public int X;
        public int Y;
        public int Height;
        public int Width;

        public Point Location => new Point(X, Y);
        public Point Size => new Point(Width, Height);
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);


        public Color ForeColor;
        public Color BackgroundColor = Color.Black;

        public virtual bool Hover { get; set; }
        public virtual bool Pressed { get; set; }
        public object Value { get; set; }

        public Point PressStart;

        public event EventHandler<UIControlClickEventArgs> MousePressed;
        public event EventHandler<UIControlClickEventArgs> MouseReleased;
        public event EventHandler<UIControlMouseEventArgs> MouseEnter;
        public event EventHandler<UIControlMouseEventArgs> MouseLeave;

        public bool Enabled = true;

        public Sprite BaseTexture;
        public Sprite HoverTexture;

        public Rectangle InnerRect;

        public UIControl()
        {

        }

        public virtual void Update(ref GameTime gameTime, double timeScale, InputHelper ih)
        {
            if (Enabled)
            {
                if (Bounds.Contains(ih.MousePosition))
                {
                    if (!Hover)
                    {
                        Hover = true;
                        mouseEnter(new UIControlMouseEventArgs(this));
                    }
                    if (ih.IsNewPress(MouseButtons.LeftButton)) {
                            
                        //Trigger button press event
                        PressStart = ih.MousePosition.ToPoint() - Bounds.Location; 
                        mousePressed(new UIControlClickEventArgs(this));
                        Pressed = true;
                    }
                    else if (ih.IsOldPress(MouseButtons.LeftButton) && Pressed)
                    {
                        mouseReleased(new UIControlClickEventArgs(this));
                        Pressed = false;
                    }
                }
                else
                {
                    if (Hover)
                    {
                        Hover = false;
                        mouseLeave(new UIControlMouseEventArgs(this));
                    }
                    Pressed = false;
                }
            }
        }
        public virtual void Draw(SpriteBatch sb)
        {

        }

        /// <summary>
        /// Trigger button press event
        /// </summary>
        /// <param name="e"></param>
        private void mousePressed(UIControlClickEventArgs e)
        {
            MousePressed?.Invoke(this, e);
        }

        /// <summary>
        /// Trigger button release
        /// </summary>
        /// <param name="e"></param>
        private void mouseReleased(UIControlClickEventArgs e)
        {
            MouseReleased?.Invoke(this, e);
        }

        /// <summary>
        /// Trigger mouse enter
        /// </summary>
        /// <param name="e"></param>
        private void mouseEnter(UIControlMouseEventArgs e)
        {
            MouseEnter?.Invoke(this, e);
        }

        /// <summary>
        /// Trigger mouse leave
        /// </summary>
        /// <param name="e"></param>
        private void mouseLeave(UIControlMouseEventArgs e)
        {
            MouseLeave?.Invoke(this, e);
        }
    }
}
