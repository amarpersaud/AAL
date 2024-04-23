using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    /// <summary>
    /// Event argumetns for when a UI element is clicked
    /// </summary>
    public class UIControlClickEventArgs : EventArgs
    {
        public UIControl Sender { get; set; }
        public UIControlClickEventArgs(UIControl sender)
        {
            this.Sender = sender;
        }
    }
    /// <summary>
    /// Event arguments for when a mouse event other than a click occurs, such as entering or leaving
    /// </summary>
    public class UIControlMouseEventArgs : EventArgs
    {
        public UIControl Sender { get; set; }
        public UIControlMouseEventArgs(UIControl sender)
        {
            this.Sender = sender;
        }
    }
    /// <summary>
    /// Event arguments for when a UI element is redrawn. Useful for also updating or redrawing other or overlapping elements.
    /// </summary>
    public class UIControlPaintEventArgs : EventArgs
    {
        public UIControl Sender { get; set; }
        public UIControlPaintEventArgs(UIControl sender)
        {
            this.Sender = sender;
        }
    }
}
