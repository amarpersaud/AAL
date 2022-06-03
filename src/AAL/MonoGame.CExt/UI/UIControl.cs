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
        /// <summary>
        /// X position of top left corner
        /// </summary>
        public int X;

        /// <summary>
        /// Y position of top left corner
        /// </summary>
        public int Y;

        /// <summary>
        /// Height of control bounds
        /// </summary>
        public int Height;

        /// <summary>
        /// Width of control bounds
        /// </summary>
        public int Width;



        /// <summary>
        /// Location of top left corner
        /// </summary>
        public Point Location => new Point(X, Y);

        /// <summary>
        /// Size with width and height of control
        /// </summary>
        public Point Size => new Point(Width, Height);

        /// <summary>
        /// Rectangle representing bounds of the control
        /// </summary>
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);

        /// <summary>
        /// Borders representing directional margin between this control and neighboring controls
        /// </summary>
        public Borders Margin;

        /// <summary>
        /// Borders representing internal padding between the bounds of this control and its child controls
        /// </summary>
        public Borders Padding;


        /// <summary>
        /// Foreground Color
        /// </summary>
        public Color ForeColor = Color.White;

        /// <summary>
        /// Background Color. Affects texture draw color
        /// </summary>
        public Color BackgroundColor = Color.Black;

        /// <summary>
        /// If mouse is hovered over control but not pressed
        /// </summary>
        public virtual bool Hover { get; set; }

        /// <summary>
        /// If mouse is on control and control clicked
        /// </summary>
        public virtual bool Pressed { get; set; }

        /// <summary>
        /// Value for input controls
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Location where click first began relative to top left corner
        /// </summary>
        public Point PressStart;

        /// <summary>
        /// Event triggered if Mouse is pressed on the control
        /// </summary>
        public event EventHandler<UIControlClickEventArgs> MousePressed;

        /// <summary>
        /// Even triggered if the mouse is released on the control after the mouse has been pressed
        /// </summary>
        public event EventHandler<UIControlClickEventArgs> MouseReleased;

        /// <summary>
        /// Event triggered if the mouse enters the bounds of the control
        /// </summary>
        public event EventHandler<UIControlMouseEventArgs> MouseEnter;

        /// <summary>
        /// Event triggered if the mouse leaves the bounds of the control
        /// </summary>
        public event EventHandler<UIControlMouseEventArgs> MouseLeave;

        /// <summary>
        /// If control is enabled
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Initial Texture for the control
        /// </summary>
        public Sprite BaseTexture;

        /// <summary>
        /// Texture to use when hovering over the control
        /// </summary>
        public Sprite HoverTexture;

        /// <summary>
        /// Inner rectangle with padding
        /// </summary>
        public Rectangle InnerRect => new Rectangle(X + Padding.Left, Y + Padding.Top, X + Width - Padding.Left - Padding.Right, Y + Height - Padding.Bottom - Padding.Top);

        /// <summary>
        /// Draw order for control. If 1, this control is drawn after its parent.
        /// </summary>
        public int RelativeOrder { get; set; } = 1;

        /// <summary>
        /// Absolute draw order of the control
        /// </summary>
        public int AbsoluteOrder
        {
            get
            {
                if(Parent != null)
                {
                    return Parent.AbsoluteOrder + RelativeOrder;
                }
                return RelativeOrder;
            }
        }

        /// <summary>
        /// Parent of this control.
        /// </summary>
        public UIControl Parent { get; set; }

        /// <summary>
        /// List of child UI Controls
        /// </summary>
        public List<UIControl> Children = new List<UIControl>();

        /// <summary>
        /// Detects all mouse presses even if this control is occluded by another
        /// </summary>
        public bool DetectAllMousePresses { get; set; } = false;

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
