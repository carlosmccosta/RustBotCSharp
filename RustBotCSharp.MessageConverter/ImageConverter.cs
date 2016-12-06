using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RustBotCSharp.MessageConverter
{
    public class ImageConverter
    {
        public static WriteableBitmap ConvertToWrittableBitmap(Image image, double dpi = 96)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(image.Width, image.Height, dpi, dpi, PixelFormats.Bgr24, null);
            writeableBitmap.Lock();
            unsafe
            {
                byte* pBackBuffer = (byte*)writeableBitmap.BackBuffer;
                foreach (var pixel in image.Data)
                {
                    *pBackBuffer++ = (byte)pixel;
                }
            }

            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, image.Width, image.Height));
            writeableBitmap.Unlock();
            writeableBitmap.Freeze();
            return writeableBitmap;
        }
    }
}
