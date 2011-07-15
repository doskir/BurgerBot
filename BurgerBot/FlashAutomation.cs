using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace BurgerBot
{
    class FlashAutomation
    {
        // ReSharper disable InconsistentNaming
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public enum PostMessageFlags
        {
            MK_LBUTTON = 0x0001,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200
        }

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        // ReSharper restore InconsistentNaming
        public static IntPtr FlashHandle;
        public static AxShockwaveFlashObjects.AxShockwaveFlash FlashObject;
        public static void Click(int x, int y)
        {
            var pos = new IntPtr(y * 0x10000 + x);
            var handle = new HandleRef(FlashObject, FlashHandle);
            PostMessage(handle, (uint)PostMessageFlags.WM_LBUTTONDOWN,
                        new IntPtr((int)PostMessageFlags.MK_LBUTTON), pos);
            System.Threading.Thread.Sleep(10);
            PostMessage(handle, (int)PostMessageFlags.WM_LBUTTONUP, new IntPtr((int)PostMessageFlags.MK_LBUTTON), pos);
            System.Threading.Thread.Sleep(10);
            Log.AddMessage("Clicked " + x + "," + y,Log.LoggingLevel.Spam);
        }

        public static void DragDrop(int srcX, int srcY, int destX, int destY)
        {
            var srcPos = new IntPtr(srcY * 0x10000 + srcX);
            var destPos = new IntPtr(destY * 0x10000 + destX);
            var handle = new HandleRef(FlashObject, FlashHandle);
            //pick up
            PostMessage(handle, (uint) PostMessageFlags.WM_LBUTTONDOWN, IntPtr.Zero, srcPos);
            System.Threading.Thread.Sleep(10);

            //drop
            PostMessage(handle, (uint) PostMessageFlags.WM_LBUTTONUP, IntPtr.Zero, destPos);
            System.Threading.Thread.Sleep(10);
            Log.AddMessage("Dragged from " + srcX + ',' + srcY + " to " + destX + ',' + destY, Log.LoggingLevel.Spam);
        }
        public static void AntiHover(int srcX,int srcY)
        {
            Log.AddMessage("Clearing Hover", Log.LoggingLevel.Spam);
            var handle = new HandleRef(FlashObject, FlashHandle);
            int x = srcX;
            int y = srcY;
            while (x >= 0 && y >= 0)
            {
                var destPos = new IntPtr(y*0x10000 + x);
                PostMessage(handle, (uint) PostMessageFlags.WM_MOUSEMOVE, IntPtr.Zero, destPos);
                System.Threading.Thread.Sleep(5);
                x -= 10;
                y -= 10;
            }
            Log.AddMessage("Cleared Hover",Log.LoggingLevel.Spam);
        }

        public static Bitmap ScreenShot()
        {
            Log.AddMessage("Taking Screenshot", Log.LoggingLevel.Spam);
            Graphics g = FlashObject.CreateGraphics();
            var bmp = new Bitmap(FlashObject.Size.Width, FlashObject.Size.Height, g);
            Graphics memoryGraphics = Graphics.FromImage(bmp);
            IntPtr dc = memoryGraphics.GetHdc();
            PrintWindow(FlashHandle, dc, 0);
            memoryGraphics.ReleaseHdc(dc);
            // bmp now contains the screenshot
            return bmp;
        }
    }
}
