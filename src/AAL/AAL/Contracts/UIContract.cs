using System;
using System.Collections.Generic;
using System.Text;

using MonoGame.CExt;
using MonoGame.CExt.UI;
using MonoGame.CExt.Utility;
using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
using MonoGame.CExt.Extensions;

namespace AAL.Contracts
{
    /// <summary>
    /// A UI Control for physically displaying the contract on screen
    /// </summary>
    public class UIContract : UIControl
    {
        /// <summary>
        /// Contract object that holds contract information
        /// </summary>
        public Contract BaseContract; 

        public UIContract() : base()
        {

        }

    }
}
