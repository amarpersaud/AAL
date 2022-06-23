using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    public class UIControlClickEventArgs : EventArgs
    {
        public UIControl Sender { get; set; }
        public UIControlClickEventArgs(UIControl sender)
        {
            this.Sender = sender;
        }
    }
    public class UIControlMouseEventArgs : EventArgs
    {
        public UIControl Sender { get; set; }
        public UIControlMouseEventArgs(UIControl sender)
        {
            this.Sender = sender;
        }
    }
    public class UIControlPaintEventArgs : EventArgs
    {
        public UIControl Sender { get; set; }
        public UIControlPaintEventArgs(UIControl sender)
        {
            this.Sender = sender;
        }
    }
}
