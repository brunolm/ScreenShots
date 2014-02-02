using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ScreenShots
{
    public class ScreenManager
    {
        /// <summary>
        /// Take a screenshot from all screens.
        /// </summary>
        /// <returns>Concatenated image from all screens.</returns>
        public static Bitmap TakeScreenshot()
        {
            Rectangle totalSize = Rectangle.Empty;

            foreach (var screen in Screen.AllScreens)
            {
                totalSize = Rectangle.Union(totalSize, screen.Bounds);
            }

            Bitmap screenShotBMP = new Bitmap(totalSize.Width, totalSize.Height, PixelFormat.Format32bppArgb);

            Graphics screenShotGraphics = Graphics.FromImage(screenShotBMP);

            screenShotGraphics.CopyFromScreen(totalSize.X, totalSize.Y, 0, 0, totalSize.Size, CopyPixelOperation.SourceCopy);

            screenShotGraphics.Dispose();

            return screenShotBMP;
        }
    }
}
