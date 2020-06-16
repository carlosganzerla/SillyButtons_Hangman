using SillyButtons.Abstractions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace SillyButtons.Modules.Tests
{
    public static class TestExtensions
    {
        public static void CleanUpDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Directory.Delete(directoryPath, true);
            }
        }
        public static void MakeGuess(this IHangmanGame game, string guess)
        {
            for (int i = 0; i < guess.Length; i++)
            {
                game.MakeGuess(guess[i]);
            }
        }

        [DllImport("msvcrt.dll")]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        public static bool BitmapEquals(this Bitmap current, Bitmap other)
        {
            if ((current == null) != (other == null)) return false;
            if (current.Size != other.Size) return false;
            var bd1 = current.LockBits(new Rectangle(new Point(0, 0), current.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = other.LockBits(new Rectangle(new Point(0, 0), other.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;
                int stride = bd1.Stride;
                int len = stride * current.Height;
                return memcmp(bd1scan0, bd2scan0, len) == 0;
            }
            finally
            {
                current.UnlockBits(bd1);
                other.UnlockBits(bd2);
            }
        }
    }
}
