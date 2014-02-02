using System.Drawing;

namespace ScreenShots
{
    public static class ImageExtender
    {
        /// <summary>
        /// Creates a new image from a portion of the source image.
        /// </summary>
        /// <param name="img">Source image.</param>
        /// <param name="area">Area to be extracted from the source image.</param>
        /// <returns>A new image with the contents of the selected area.</returns>
        public static Image Crop(this Image img, Rectangle area)
        {
            var target = new Bitmap(area.Width, area.Height);

            using (var g = Graphics.FromImage(target))
            {
                g.DrawImage(img, -area.X, -area.Y);
            }

            return target;
        }
    }
}
