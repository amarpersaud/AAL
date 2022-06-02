using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    public class UIControlClickEventArgs : EventArgs
    {
        public UIControl sender { get; set; }
        public UIControlClickEventArgs(UIControl sender)
        {
            this.sender = sender;
        }
    }
    public class UIControlMouseEventArgs : EventArgs
    {
        public UIControl sender { get; set; }
        public UIControlMouseEventArgs(UIControl sender)
        {
            this.sender = sender;
        }
    }
}
