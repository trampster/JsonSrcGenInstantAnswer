using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JsonSrcGenInstantAnswer.Tests
{
   public static class BitmapTestHelpers
   {
      static BitmapImage ToBitmapImage(BitmapSource bitmapSource)
      {
         JpegBitmapEncoder encoder = new JpegBitmapEncoder();
         var memoryStream = new MemoryStream();
         var bImg = new BitmapImage();

         encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
         encoder.Save(memoryStream);

         memoryStream.Position = 0;
         bImg.BeginInit();
         bImg.StreamSource = memoryStream;
         bImg.EndInit();

         memoryStream.Close();

         return bImg;
      }

      public static BitmapImage CreateBitmapImage()
      {
         var bitmapSource = BitmapImage.Create(
             2,
             2,
             96,
             96,
             PixelFormats.Indexed1,
             new BitmapPalette(new List<Color> { Colors.Transparent }),
             new byte[] { 0, 0, 0, 0 },
             1);
         return ToBitmapImage(bitmapSource);
      }
   }
}
