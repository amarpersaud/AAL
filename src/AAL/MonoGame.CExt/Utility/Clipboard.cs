using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonoGame.CExt.Utility
{
    /// <summary>
    /// Wrapper class for Clipboard.
    /// TODO: Make this cross platform
    /// </summary>
    public static class Clipboard
    {

#if (!LINUX&&!XBOX)
        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern bool SetClipboardData(uint uFormat, IntPtr data);

        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardData(uint uFormat);
#endif

        public static string GetClipboardText()
        {
            var str = "";

#if (!LINUX&&!XBOX)
            OpenClipboard(IntPtr.Zero);
            var ptr = GetClipboardData(13);
            str = Marshal.PtrToStringUni(ptr);
            CloseClipboard();
#endif
            return str;
        }
        public static bool SetClipboardText(string Text)
        {
#if (!LINUX&&!XBOX)           
            OpenClipboard(IntPtr.Zero);
            var ptr = Marshal.StringToHGlobalUni(Text);
            SetClipboardData(13, ptr);
            CloseClipboard();
            Marshal.FreeHGlobal(ptr);
            return true;
#endif
            return false;
        }
    }
}
