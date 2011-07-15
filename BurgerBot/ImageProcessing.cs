using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace BurgerBot
{
    class ImageProcessing
    {
        public static Bitmap GetRegion(Bitmap bmp,Rectangle rct)
        {
            Bitmap result = bmp.Clone(rct, PixelFormat.Format32bppArgb);
            return result;
        }
        public static bool CompareBitmaps(Bitmap b1, Bitmap b2)
        {
            if (b1.Width != b2.Width || b1.Height != b2.Height)
                return false;
            if (b1.PixelFormat != b2.PixelFormat)
                return false;
            int bytes;

            if (b1.PixelFormat == PixelFormat.Format32bppArgb && b2.PixelFormat == PixelFormat.Format32bppArgb)
            {
                bytes = b1.Width * b1.Height * 4;
            }
            else
            {
                throw new NotImplementedException("Unsupported PixelFormat passed to CompareBitmaps");
            }
            bool result = true;
            var b1Bytes = new byte[bytes];
            var b2Bytes = new byte[bytes];
            BitmapData bmd1 = b1.LockBits(new Rectangle(0, 0, b1.Width - 1, b1.Height - 1), ImageLockMode.ReadOnly, b1.PixelFormat);
            BitmapData bmd2 = b2.LockBits(new Rectangle(0, 0, b2.Width - 1, b2.Height - 1), ImageLockMode.ReadOnly, b2.PixelFormat);
            System.Runtime.InteropServices.Marshal.Copy(bmd1.Scan0, b1Bytes, 0, bytes);
            System.Runtime.InteropServices.Marshal.Copy(bmd2.Scan0, b2Bytes, 0, bytes);
            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1Bytes[n] != b2Bytes[n])
                {
                    result = false;
                    break;
                }
            }
            b1.UnlockBits(bmd1);
            b2.UnlockBits(bmd2);
            return result;
        }
    }
}