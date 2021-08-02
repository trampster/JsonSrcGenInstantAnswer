using System.Windows.Media.Imaging;

namespace JsonSrcGenInstantAnswer.Services
{
   public interface IBitmapBuilder
   {
      BitmapImage LoadImage(byte[] imageData);
   }
}