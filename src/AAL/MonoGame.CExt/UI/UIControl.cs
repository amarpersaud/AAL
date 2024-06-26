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
using MonoGame.CExt.Utility;

namespace MonoGame.CExt.UI
{
    /// <summary>
    /// Base UIControl class. Implements shared fields, methods, and events for drawing, interacting with and mananging UI elements.
    /// </summary>
    public abstract class UIControl
    {
        #region Resources

        protected readonly ResourceHandler _rh;
        protected SpriteBatch sb => _rh._sb;
        protected InputHelper ih => _rh._ih;

        #endregion Resources

        #region subfields
        private int _x;
        private int _y;
        private int _height;
        private int _width;
        private UIControl _parent;
        private string _fontName;
        #endregion subfields

        #region Position

        /// <summary>
        /// X position of top left corner. Does not affect width of control
        /// </summary>
        public int X
        {
            get { return _x; }
            set
            {
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
                        Anchor.RightDistance = Parent.InnerRect.Width - value - Width;
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
                        temp.BottomDistance = Parent.InnerRect.Height - value - Height;
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
                if(Parent != null)
                {
                    if (Anchor.Bottom)
                    {
                        if (Anchor.Top)
                        {
                            //Top and bottom both anchored. Dont change height since its not possible here.
                        }
                        else
                        {
                            //Bottom anchored, top not anchored. Change y position and height
                            _height = value;
                            _y = Parent.Height - value - Anchor.BottomDistance;
                        }
                    }
                    else
                    {
                        //Regardless of top anchor, change height
                        _height = value;
                    }
                }
                else
                {
                    _height = value;
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
                if (Parent != null)
                {
                    if (Anchor.Right)
                    {
                        if (Anchor.Left)
                        {
                            //left and right both anchored. Dont change width since its not possible here.
                        }
                        else
                        {
                            //right anchored, left not anchored. Change x position and width
                            _width = value;
                            _x = Parent.Width - value - Anchor.RightDistance;
                        }
                    }
                    else
                    {
                        //Regardless of right anchor, change width
                        _width = value;
                    }
                }
                else
                {
                    _width = value;
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
                int targetWidth = _width + _x - value;

                if(targetWidth < 0)
                {
                    throw new Exception("Setting the left value of UI Control creates a negative width control");
                }

                //Change width to keep right side in same place
                _width = targetWidth;

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
        /// X Offset for children. Applied to screen coordinates.
        /// </summary>
        public int ChildOffsetX;

        /// <summary>
        /// Y Offset for children. Applied to screen coordinates.
        /// </summary>
        public int ChildOffsetY;

        /// <summary>
        /// Offset of child controls. Used for scrolling. Applied to screen coordinates.
        /// </summary>
        public Point ChildOffset => new Point(ChildOffsetX, ChildOffsetY);

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
                return Parent.ScreenX + X + Parent.ChildOffsetX + Parent.Padding.Left;
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
                return Parent.ScreenY + Y + Parent.ChildOffsetY + Parent.Padding.Top;
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
        /// Anchors for edges to parent control
        /// </summary>
        public Anchor Anchor = Anchor.None;

        /// <summary>
        /// Rectangle representing bounds of the control relative to parent control
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(X, Y, Width, Height); }
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>
        /// Rectangle representing bounds of the control in screen coordinates
        /// </summary>
        public Rectangle ScreenBounds => new Rectangle(ScreenX, ScreenY, Width, Height);

        /// <summary>
        /// Borders representing directional margin between this control and neighboring controls
        /// </summary>
        public Borders Margin = Borders.Zero;

        /// <summary>
        /// Borders representing internal padding between the bounds of this control and its child controls
        /// </summary>
        public Borders Padding = Borders.Zero;

        #region Additional Regions

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

        #endregion Additional Regions

        #endregion Position

        #region Appearance

        /// <summary>
        /// Foreground Color
        /// </summary>
        public Color ForeColor = Color.White;

        /// <summary>
        /// Background Color. Affects texture draw color
        /// </summary>
        public Color BackgroundColor = Color.Black;

        /// <summary>
        /// Initial Sprite for the control
        /// </summary>
        public Sprite BackgroundSprite;

        /// <summary>
        /// Font of the control's text
        /// </summary>
        public SpriteFont Font { get; private set; }

        /// <summary>
        /// Name of the font
        /// </summary>
        public string FontName {
            get { return _fontName; }
            set
            {
                _fontName = value;
                Font = _rh.GetFont(value);
            }
        
        }

        public const string DefaultFontName = "Arial";

        #endregion Appearance

        #region Events

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
        /// Event triggered if control is invalidated
        /// </summary>
        public event EventHandler<UIControlPaintEventArgs> Invalidated;

        #endregion Events

        #region Status

        /// <summary>
        /// Location where click first began relative to top left corner
        /// </summary>
        public Point PressStart;

        /// <summary>
        /// If control is enabled
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// If control is drawn
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Overflow is visible by default
        /// </summary>
        public UIOverflow Overflow = UIOverflow.Visible;

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
        /// Is root element in a tree
        /// </summary>
        public bool IsRoot => Parent == null;

        public virtual bool Selectable { get; set; } = true;

        #endregion Status

        /// <summary>
        /// Parent of this control.
        /// </summary>
        public UIControl Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
                Invalidate();
            }
        }

        /// <summary>
        /// List of child UI Controls
        /// </summary>
        public List<UIControl> Children = new List<UIControl>();

        public UIControl(ResourceHandler resourceHandler)
        {
            this._rh = resourceHandler;
            FontName = DefaultFontName;
        }

        /// <summary>
        /// Updates this control
        /// </summary>
        /// <param name="gameTime">Time elapsed</param>
        /// <param name="timeScale">Time elapsed as a double</param>
        /// <param name="ih">Input helper instance</param>
        /// <param name="uih">Root UI handler</param>
        /// <exception cref="ArgumentNullException">Thrown if no UI handler is supplied</exception>
        public virtual void Update(GameTime gameTime, double timeScale, UIHandler uih)
        {
            if (!Enabled)
            {
                return;
            }

            if (uih == null)
            {
                throw new ArgumentNullException("uih", "root UIHandler cannot be null");
            }

            //Mouse is inside this element
            if(this == uih.CurrentMouseControl)
            {
                if (!Hover)
                {
                    Hover = true;                                           //Mouse is now in control
                    OnMouseEnter(new UIControlMouseEventArgs(this));        //Trigger mouse enter event
                }
                if (ih.IsNewPress(MouseButtons.LeftButton))
                {
                    //Store location where click initially occurred. Useful for dragging/dropping.
                    PressStart = ih.MousePosition.ToPoint() - Bounds.Location;
                       
                    OnMousePressed(new UIControlClickEventArgs(this));      //Trigger button press event
                        
                    Pressed = true;                                         //Set this control as pressed

                    if (Selectable)
                    {
                        uih.SelectedControl = this;                             //Set this control as the new selected control
                    }
                    else
                    {
                        //Find most recent selectable ancestor.
                        UIControl c = Parent;
                        while(c.Parent != null && !c.Selectable)
                        {
                            c = c.Parent;
                        }
                        if(c != null && c.Selectable)
                        {
                            uih.SelectedControl = c;        //Breaks if controls overlap. Fix in One_Button
                        }
                    }
                }
                else if (ih.IsOldPress(MouseButtons.LeftButton) && Pressed)
                {
                    OnMouseReleased(new UIControlClickEventArgs(this));     //Trigger mouse release event
                    Pressed = false;                                        //Mouse no longer pressed
                }
            }
            else
            {
                //If mouse was hovering over the element before and now is not
                if (Hover)
                {
                    //mouse has left control
                    OnMouseLeave(new UIControlMouseEventArgs(this));
                }
                //Disable hover and pressing
                Hover = false;
                Pressed = false;
            }
        }
        
        /// <summary>
        /// Draws this control to screen.
        /// </summary>
        /// <param name="sb">Sprite batch for drawing textures</param>
        public virtual void Draw()
        {

        }

        #region Event Caller Functions

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

        private void OnInvalidate(UIControlPaintEventArgs e)
        {
            Invalidated?.Invoke(this, e);
        }

        #endregion Event Caller Functions

        #region Descendant

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
                    //If control is visible regardless of enabled status
                    if (c.Visible)
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
            if (Children == null || index >= Children.Count || Children.Count == 0)
            {
                return;
            }

            //Copy list
            UIControl[] temp = new UIControl[Children.Count];
            Children.CopyTo(temp);
            List<UIControl> newList = temp.ToList();
            
            UIControl c = temp.ElementAtOrDefault(index);
            newList.RemoveAt(index);
            newList.Add(c);

            this.Children = newList;
        }
        public void BringChildToFront(UIControl c)
        {
            if (Children == null || c == null || !Children.Contains(c))
            {
                return;
            }

            //Copy list
            UIControl[] temp = new UIControl[Children.Count];
            Children.CopyTo(temp);
            List<UIControl> newList = temp.ToList();

            newList.Remove(c);
            newList.Add(c);

            this.Children = newList;
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
                    c.Invalidate();
                }
            }
        }

        public void AddControl(UIControl c)
        {
            c.Parent = this;
            if(Children is null)
            {
                Children = new List<UIControl>();
            }
            Children.Add(c);
        }

        #endregion Descendant

        #region Paint

        /// <summary>
        /// Updates this control's variables then updates all children
        /// Called when bounds and internal variables need to be updated
        /// such as when the parent's bounds have changed.
        /// </summary>
        public void Invalidate()
        {
            if (Parent is null)
            {
                //If the parent is null then this is a root element. Update children only
                this.UpdateChildControlBounds();
                return;
            }

            //Otherwise, the parent has updated it bounds, and we update only this control's bounds.
            this.ControlInvalidated();

            //Update children's control bounds
            this.UpdateChildControlBounds();
        }

        /// <summary>
        /// Updates internal variables. Occurs before children are updated when
        /// control is invalidated.
        /// </summary>
        protected virtual void ControlInvalidated()
        {
            //If left anchored
            if (Anchor.Left)
            {
                //both left and right anchored
                if (Anchor.Right)
                {
                    //Find new width
                    int NewWidth = Parent.InnerRect.Width - Anchor.RightDistance - Anchor.LeftDistance;

                    //Make sure control width >= 0
                    _width = MathExt.Max(NewWidth, 0);
                    _x = Anchor.LeftDistance;
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
                    //Update left position of control
                    _x = Parent.InnerRect.Width - Anchor.RightDistance - this.Width;
                }
                else
                {
                    //Neither is anchored. Control should move horizontally so it occupies same position scale wise
                    // and maintain the same width.

                    //Find percentage of distance from left edge of parent
                    //double p = (Anchor.LeftDistance) / (Anchor.LeftDistance + Anchor.RightDistance + Width);

                    //Find new x position
                    //_x = (int)(p * Parent.InnerRect.Width);

                    //Update anchors
                    //Anchor newAnchor = this.Anchor;

                    //newAnchor.LeftDistance = _x;
                    //newAnchor.RightDistance = Parent.InnerRect.Width - Anchor.LeftDistance - Width;

                    //this.Anchor = newAnchor;
                }
            }

            //If top anchored
            if (Anchor.Top)
            {
                //both top and bottom anchored
                if (Anchor.Bottom)
                {
                    //Find new Height
                    int NewHeight = Parent.InnerRect.Height - Anchor.BottomDistance - Anchor.TopDistance;

                    //Make sure control height >= 0
                    _height = MathExt.Max(NewHeight, 0);
                    _y = Anchor.TopDistance;
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
                    //Update left position of control
                    _y = Parent.InnerRect.Height - Anchor.BottomDistance - this.Height;
                }
                else
                {
                    //Neither is anchored. Control should move vertically so it occupies same position scale wise

                    //Find percentage of distance from bottom edge of parent
                    //double p = (Anchor.TopDistance) / (Anchor.TopDistance + Anchor.BottomDistance + Height);

                    //Find new y position
                    //_y = (int)(p * Parent.InnerRect.Height);

                    //Update anchors
                    //Anchor newAnchor = this.Anchor;

                    //newAnchor.TopDistance = _y;
                    //newAnchor.BottomDistance = Parent.InnerRect.Height - Anchor.TopDistance - Height;

                    //this.Anchor = newAnchor;
                }
            }

            OnInvalidate(new UIControlPaintEventArgs(this));
        }


        public virtual void Show()
        {
            this.Visible = true;
            this.Enabled = true;

            if(Parent != null)
            {
                Parent.BringChildToFront(this);
            }
        }

        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        #endregion Paint

        /// <summary>
        /// Anchors one side of the control to its parent
        /// </summary>
        /// <param name="side">Side to set anchor on</param>
        /// <param name="Distance">Distance from edge to set anchor. null uses current distance from edge. </param>
        /// <returns>True if anchor is set. False otherwise</returns>
        public bool SetAnchor(Side side, int? Distance = null)
        {
            if (Parent != null) {
                if (Distance == null)
                {
                    //Set to current distance.
                    switch (side)
                    {
                        case Side.Left:
                            Anchor.Left = true;
                            Anchor.LeftDistance = X;
                            break;
                        case Side.Top:
                            Anchor.Top = true;
                            Anchor.TopDistance = Y;
                            break;
                        case Side.Right:
                            Anchor.Right = true;
                            Anchor.RightDistance = Parent.InnerRect.Width - Right;
                            break;
                        case Side.Bottom:
                            Anchor.Bottom = true;
                            Anchor.BottomDistance = Parent.InnerRect.Height - Bottom;
                            break;
                    }
                }
                else
                {
                    switch (side)
                    {
                        case Side.Left:
                            Anchor.Left = true;
                            Anchor.LeftDistance = (int)Distance;
                            break;
                        case Side.Top:
                            Anchor.Top = true;
                            Anchor.TopDistance = (int)Distance;
                            break;
                        case Side.Right:
                            Anchor.Right = true;
                            Anchor.RightDistance = (int)Distance;
                            break;
                        case Side.Bottom:
                            Anchor.Bottom = true;
                            Anchor.BottomDistance = (int)Distance;
                            break;
                    }
                }
                Invalidate();
                return true;
            }
            return false;
        }
        public void ClearAnchor(Side side)
        {
            switch (side)
            {
                case Side.Left:
                    Anchor.Left = false;
                    break;
                case Side.Right:
                    Anchor.Right = false;
                    break;
                case Side.Top:
                    Anchor.Top = false;
                    break;
                case Side.Bottom:
                    Anchor.Bottom = false;
                    break;

            }
        }

    }
}
