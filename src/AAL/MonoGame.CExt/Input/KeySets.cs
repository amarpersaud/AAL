using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.Input
{
    public static class KeySets
    {
        public static HashSet<Keys> Special = new HashSet<Keys> {
            Keys.OemPeriod, Keys.OemMinus, Keys.Decimal, Keys.OemPlus, Keys.Multiply,
            Keys.Add, Keys.Divide, Keys.Subtract,Keys.OemSemicolon,Keys.OemQuestion,
            Keys.OemQuotes,Keys.OemPipe,Keys.OemCloseBrackets, Keys.OemOpenBrackets,
            Keys.OemComma, Keys.OemBackslash,Keys.OemTilde
        };

        public static HashSet<Keys> Letters = new HashSet<Keys>
        {
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
            Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
            Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
            Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
            Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
            Keys.Z
        };
        public static HashSet<Keys> Numbers = new HashSet<Keys>
        {
            Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4,
            Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9
        };
        public static HashSet<Keys> Numpad = new HashSet<Keys>
        {
            Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4,
            Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9
        };
        public static HashSet<Keys> Other = new HashSet<Keys>
        {
            Keys.Back
        };

    }
}
