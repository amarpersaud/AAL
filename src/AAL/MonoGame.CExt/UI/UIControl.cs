﻿using System;
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
        #region subfields
        private int _x;
        private int _y;
        private int _height;
        private int _width;
        #endregion subfields

        /// <summary>
        /// X position of top left corner. Does not affect width of control
        /// </summary>
        public int X
        {
            get { return _x; }
            set
            {
                OldBounds = Bounds;
                OldScreenBounds = ScreenBounds;

                //Update anchors
                if (Parent != null)
                {
                    Anchor temp = this.Anchor;
                    if (Anchor.Left)
                    {
                        Anchor.LeftDistance = value;
                    }
                    if (Anchor.Right)
                    {
                        Anchor.RightDistance = Parent.Width - value - Width;
                    }
                    this.Anchor = temp;
                }

                _x = value;

                UpdateChildControlBounds();
            }
        }

        /// <summary>
        /// Y position of top left corner. Does not affect height of control
        /// </summary>
        public int Y
        {
            get { return _y; }
            set
            {
                OldBounds = Bounds;
                OldScreenBounds = ScreenBounds;

                //Update anchors
                if (Parent != null)
                {
                    Anchor temp = this.Anchor;
                    if (Anchor.Top)
                    {
                        temp.TopDistance = value;
                    }
                    if (Anchor.Bottom)
                    {
                        temp.BottomDistance = Parent.Height - value - Height;
                    }
                    this.Anchor = temp;
                }

                _y = value;

                UpdateChildControlBounds();
            }
        }

        /// <summary>
        /// Height of control bounds
        /// </summary>
        public int Height
        {
            get { return _height; }
            set
            {
                OldBounds = Bounds;
                OldScreenBounds = ScreenBounds;

                if(Parent != null)
                {
                    Anchor temp = Anchor;
                    if (Anchor.Bottom)
                    {
                        if (Anchor.Top)
                        {
                            //Top and bottom both anchored. Dont change height since its not possible here.
                        }
                        else
                        {
                            //Bottom anchored, top not anchored. Change y position and height
                            _y += _height - value;
                            _height = value;
                        }
                    }
                    else
                    {
                        //Regardless of top anchor, change height
                        _height = value;
                    }
                }

                UpdateChildControlBounds();
            }
        }

        /// <summary>
        /// Width of control bounds
        /// </summary>
        public int Width
        {
            get { return _width; }
            set
            {
                OldBounds = Bounds;
                OldScreenBounds = ScreenBounds;

                if (Parent != null)
                {
                    Anchor temp = Anchor;
                    if (Anchor.Right)
                    {
                        if (Anchor.Left)
                        {
                            //left and right both anchored. Dont change width since its not possible here.
                        }
                        else
                        {
                            //right anchored, left not anchored. Change x position and width
                            _x += _width - value;
                            _width = value;
                        }
                    }
                    else
                    {
                        //Regardless of right anchor, change width
                        _width = value;
                    }
                }

                UpdateChildControlBounds();
            }
        }

        /// <summary>
        /// Left bound of the control. Changing this does affect the width.
        /// </summary>
        public int Left
        {
            get { return X; }
            set
            {
                int tw = _width + _x - value;

                if(tw < 0)
                {
                    throw new Exception("Setting the left value of UI Control creates a negative width control");
                }

                //Change width to keep right side in same place
                _width = tw;

                //Update x position
                _x = value;

                //Update anchor left distance if anchor is set
                if (Anchor.Left)
                {
                    Anchor.LeftDistance = value;
                }

                UpdateChildControlBounds();
            }
        }
        
        /// <summary>
        /// Right bound of the control relative to the parent.
        /// </summary>
        public int Right
        {
            get { return X+Width; }
            set
            {
                //Find width which would be generated by setting the right side of this control to the incoming value
                int tw = value - _x;

                if(tw < 0)
                {
                    throw new Exception("Setting the right value of UI Control creates a negative width control");
                }

                //Change width
                _width = tw;

                //Update anchor right distance if anchor is set
                if (Anchor.Right)
                {
                    Anchor.RightDistance = value;
                }

                UpdateChildControlBounds();
            }
        }

        /// <summary>
        /// Top bound of the control. Changing this does affect the width.
        /// </summary>
        public int Top
        {
            get { return Y; }
            set
            {
                int th = _height + _y - value;
                if(th < 0)
                {
                    throw new Exception("Setting the top value of UI Control creates a negative height control.");
                }

                //Change height to keep bottom side in same place
                _height = th;

                //Update y position
                _y = value;

                //Update anchor top distance if anchor is set
                if (Anchor.Top)
                {
                    Anchor.TopDistance = value;
                }

                UpdateChildControlBounds();
            }
        }

        /// <summary>
        /// Bottom edge of UI control relative to parent.
        /// </summary>
        public int Bottom
        {
            get { return Y+Height; }
            set
            {
                //Find height which would be generated by setting the bottom side of this control to the incoming value
                int th = value - _y;

                if (th < 0)
                {
                    throw new Exception("Setting the bottom value of UI Control creates a negative height control");
                }

                //Change height
                _height = th;

                //Update anchor bottom distance if anchor is set
                if (Anchor.Bottom)
                {
                    Anchor.BottomDistance = value;
                }

                UpdateChildControlBounds();
            }
        }
       
        /// <summary>
        /// X position in screen coordinates
        /// </summary>
        public int ScreenX
        {
            get
            {
                if(Parent is null)
                {
                    return X;
                }
                return Parent.ScreenX + X;
            }
        }

        /// <summary>
        /// Y position in screen coordinates
        /// </summary>
        public int ScreenY
        {
            get
            {
                if (Parent is null)
                {
                    return Y;
                }
                return Parent.ScreenY + X;
            }
        }

        /// <summary>
        /// Location of top left corner
        /// </summary>
        public Point Location => new Point(X, Y);

        /// <summary>
        /// Location of top left corner
        /// </summary>
        public Point ScreenLocation => new Point(ScreenX, ScreenY);

        /// <summary>
        /// Size with width and height of control
        /// </summary>
        public Point Size => new Point(Width, Height);

        /// <summary>
        /// Borders representing directional margin between this control and neighboring controls
        /// </summary>
        public Borders Margin = Borders.Zero;

        /// <summary>
        /// Borders representing internal padding between the bounds of this control and its child controls
        /// </summary>
        public Borders Padding = Borders.Zero;

        /// <summary>
        /// Anchors for edges to parent control
        /// </summary>
        public Anchor Anchor = Anchor.Default;

        /// <summary>
        /// Rectangle representing bounds of the control relative to parent control
        /// </summary>
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);

        /// <summary>
        /// Rectangle representing bounds of the control in screen coordinates
        /// </summary>
        public Rectangle ScreenBounds => new Rectangle(X, Y, Width, Height);

        /// <summary>
        /// Former relative bounds of UI control 
        /// </summary>
        public Rectangle OldBounds { get; private set; }
        
        /// <summary>
        /// Former screen coordinate bounds of UI control
        /// </summary>
        public Rectangle OldScreenBounds { get; private set; }


        /// <summary>
        /// Inner rectangle with padding
        /// </summary>
        public Rectangle InnerRect => new Rectangle(X + Padding.Left, Y + Padding.Top, Width - Padding.Left - Padding.Right, Height - Padding.Bottom - Padding.Top);

        /// <summary>
        /// Inner rectangle with padding in screen coordinates
        /// </summary>
        public Rectangle ScreenInnerRect => new Rectangle(ScreenX + Padding.Left, ScreenY + Padding.Top, Width - Padding.Left - Padding.Right, Height - Padding.Bottom - Padding.Top);


        /// <summary>
        /// Outer rectangle including margin
        /// </summary>
        public Rectangle OuterRect => new Rectangle(X - Margin.Left, Y - Margin.Top, Width + Margin.Left + Margin.Right, Height + Margin.Top + Margin.Bottom);

        /// <summary>
        /// Outer rectangle including margin in screen coordinates
        /// </summary>
        public Rectangle ScreenOuterRect => new Rectangle(ScreenX - Margin.Left, ScreenY - Margin.Top, Width + Margin.Left + Margin.Right, Height + Margin.Top + Margin.Bottom);


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
        /// If control is drawn
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Initial Texture for the control
        /// </summary>
        public Sprite BaseTexture;

        /// <summary>
        /// Texture to use when hovering over the control
        /// </summary>
        public Sprite HoverTexture;

        /// <summary>
        /// Parent of this control.
        /// </summary>
        public UIControl Parent { get; set; }

        /// <summary>
        /// List of child UI Controls
        /// </summary>
        public List<UIControl> Children = new List<UIControl>();

        /// <summary>
        /// Is root element in a tree
        /// </summary>
        public bool IsRoot => Parent == null; 

        public UIControl()
        {

        }

        public virtual void Update(GameTime gameTime, double timeScale, InputHelper ih, UIHandler uih)
        {
            if (Enabled)
            {

                if(uih == null)
                {
                    throw new ArgumentNullException("uih", "root UIHandler cannot be null");
                }

                //Mouse is inside this element
                if(this == uih.CurrentMouseControl)
                {
                    if (!Hover)
                    {
                        Hover = true;
                        OnMouseEnter(new UIControlMouseEventArgs(this));
                    }
                    if (ih.IsNewPress(MouseButtons.LeftButton))
                    {
                        //Trigger button press event
                        PressStart = ih.MousePosition.ToPoint() - Bounds.Location;
                        OnMousePressed(new UIControlClickEventArgs(this));
                        Pressed = true;
                    }
                    else if (ih.IsOldPress(MouseButtons.LeftButton) && Pressed)
                    {
                        OnMouseReleased(new UIControlClickEventArgs(this));
                        Pressed = false;
                    }
                }
                else
                {
                    if (Hover)
                    {
                        Hover = false;
                        OnMouseLeave(new UIControlMouseEventArgs(this));
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
        public virtual void OnMousePressed(UIControlClickEventArgs e)
        {
            MousePressed?.Invoke(this, e);
        }

        /// <summary>
        /// Trigger button release
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnMouseReleased(UIControlClickEventArgs e)
        {
            MouseReleased?.Invoke(this, e);
        }

        /// <summary>
        /// Trigger mouse enter
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnMouseEnter(UIControlMouseEventArgs e)
        {
            MouseEnter?.Invoke(this, e);
        }

        /// <summary>
        /// Trigger mouse leave
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnMouseLeave(UIControlMouseEventArgs e)
        {
            MouseLeave?.Invoke(this, e);
        }


        /// <summary>
        /// Returns Front-most Control which is visible at a given point. Sort order of the Children list affects draw and check order.
        /// </summary>
        /// <returns>UI control with highest order within this control if the point is in the control, null otherwise</returns>
        public UIControl DetermineFrontMostDescendant(Point pt)
        {
            //if point is outside of the bounds, return null.
            if (!ScreenBounds.Contains(pt))
            {
                return null;
            }

            //Otherwise the point is within this control, so we must check if the point is in a child control
            UIControl control = this;

            //If there are child elements
            if (Children != null)
            {
                //loop over each child element, check if it has a frontmost control at the point and then set the current
                //frontmost control to taht child. If two children overlap at the point, the last element is further
                //forward in both drawing and checking and therefore overwrites the frontmost control.
                foreach (UIControl c in Children)
                {
                    //Get the frontmost element in this child at the point
                    UIControl i = c.DetermineFrontMostDescendant(pt);

                    //If i contains the point
                    if (i != null)
                    {
                        //make it the new highest control.
                        control = i;
                    }
                }
            }
            //return the highest control
            return control;
        }

        /// <summary>
        /// Brings a child element to the front by moving it to the end of the Children list
        /// </summary>
        /// <param name="index">index of the child to move</param>
        public void BringChildToFront(int index)
        {
            if(Children == null || index >= Children.Count)
            {
                return;
            }
            UIControl c = Children.ElementAtOrDefault(index);
            Children.RemoveAt(index);
            Children.Add(c);
        }

        /// <summary>
        /// Updates child controls on position or size changes.
        /// </summary>
        public void UpdateChildControlBounds()
        {
            //If there are children and the list is not null
            if (!(Children is null) && Children.Count > 0)
            {
                //loop over each child and update its bounds
                foreach (var c in Children)
                {
                    c.ParentBoundsChanged();
                }
            }
        }

        /// <summary>
        /// Updates this control's bounds. Called when parent's bounds have changed.
        /// </summary>
        protected virtual void ParentBoundsChanged()
        {
            if (Parent is null)
            {
                //If the parent is null then this is a root element. Update children only
                this.UpdateChildControlBounds();
                return;
            }

            //Otherwise, the parent has updated it bounds, and we update only this control's bounds.
            OldBounds = Bounds;
            OldScreenBounds = ScreenBounds;

            //Width changed
            if (Parent.Width != Parent.OldBounds.Width)
            {
                //If left anchored
                if (Anchor.Left)
                {
                    //both left and right anchored
                    if (Anchor.Right)
                    {
                        //Find new width
                        int NewWidth = Parent.Width - Anchor.RightDistance - Anchor.LeftDistance;
                        
                        //Make sure control width >= 0
                        _width = MathExt.Max(NewWidth, 0);
                    }
                    else // only left anchored
                    {
                        //Same width as before, X position is identical --> do nothing
                    }
                }
                else
                {
                    //Not left anchored and right anchored
                    if (Anchor.Right)
                    {
                        //Distance of top left from the right edge of the parent
                        int OldLeftDistance = Parent.OldBounds.Width - Anchor.LeftDistance;

                        //Update left position of control
                        _x = Parent.Width - OldLeftDistance;
                    }
                    else
                    {
                        //Neither is anchored. Control should move horizontally so it occupies same position scale wise

                        //Find percentage of distance from left edge of parent
                        double p = (Anchor.LeftDistance) / (Anchor.LeftDistance + Anchor.RightDistance + Width);
                        
                        //Find new x position
                        _x = (int)(p * Parent.Width);

                        //Update anchors
                        Anchor newAnchor = this.Anchor;

                        newAnchor.LeftDistance = _x;
                        newAnchor.RightDistance = Parent.Width - Anchor.LeftDistance - Width;

                        this.Anchor = newAnchor;
                    }
                }
            }
            else
            //Height changed
            if (Parent.Height != Parent.OldBounds.Height)
            {
                //If top anchored
                if (Anchor.Top)
                {
                    //both top and bottom anchored
                    if (Anchor.Bottom)
                    {
                        //Find new Height
                        int NewHeight = Parent.Height - Anchor.BottomDistance - Anchor.TopDistance;

                        //Make sure control height >= 0
                        _height = MathExt.Max(NewHeight, 0);
                    }
                    else // only top anchored
                    {
                        //Same height as before, relative Y position is identical --> do nothing
                    }
                }
                else
                {
                    //Not top anchored and bottom anchored
                    if (Anchor.Bottom)
                    {
                        //Distance of top left from the bottom edge of the parent
                        int OldRightDistance = Parent.OldBounds.Height - Anchor.RightDistance;

                        //Update left position of control
                        _y = Parent.Height - OldRightDistance;
                    }
                    else
                    {
                        //Neither is anchored. Control should move vertically so it occupies same position scale wise

                        //Find percentage of distance from bottom edge of parent
                        double p = (Anchor.TopDistance) / (Anchor.TopDistance + Anchor.BottomDistance + Height);

                        //Find new x position
                        _y = (int)(p * Parent.Height);

                        //Update anchors
                        Anchor newAnchor = this.Anchor;

                        newAnchor.TopDistance = _y;
                        newAnchor.BottomDistance = Parent.Height - Anchor.TopDistance - Height;

                        this.Anchor = newAnchor;
                    }
                }
            }

            //Update children's control bounds if size has have changed
            if ((Width != OldBounds.Width) || (Height != OldBounds.Height)) {
                this.UpdateChildControlBounds();
            }

        }
    }
}
