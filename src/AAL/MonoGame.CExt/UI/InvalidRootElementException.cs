using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.UI
{
    public class InvalidRootElementException : Exception
    {
        public InvalidRootElementException()
        {

        }
        public InvalidRootElementException(string message, UIControl sender) : base(message)
        {
        }

        public InvalidRootElementException(string message, UIControl sender, Exception inner)
            : base(message, inner)
        {
        }
    }
}
