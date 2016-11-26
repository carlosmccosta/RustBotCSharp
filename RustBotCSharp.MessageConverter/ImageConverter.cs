using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RustBotCSharp.MessageConverter
{
    public class ImageConverter
    {
        public static WriteableBitmap ConvertToWrittableBitmap(Image image, double dpi = 96)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(image.Width, image.Height, dpi, dpi, PixelFormats.Bgr32, null);
            writeableBitmap.Lock();
            unsafe
            {
                int pBackBuffer = (int)writeableBitmap.BackBuffer;
                foreach (Pixel pixel in image.Pixels)
                {
                    int colorData = (int)pixel.R << 16;
                    colorData |= (int)pixel.G << 8;
                    colorData |= (int)pixel.B << 0;
                    *((int*)pBackBuffer) = colorData;
                    pBackBuffer += 4;
                }
            }

            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, image.Width, image.Height));
            writeableBitmap.Unlock();
            writeableBitmap.Freeze();
            return writeableBitmap;
        }
    }
}
