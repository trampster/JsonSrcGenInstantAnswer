using JsonSrcGenInstantAnswer.Models;
using System.Threading.Tasks;

namespace JsonSrcGenInstantAnswer.Services
{
   public interface IDuckDuckGoInstantAnswerService
   {
      Task<InstantAnswer> Search(string text);

      Task<byte[]> DownloadImage(string path);
   }
}