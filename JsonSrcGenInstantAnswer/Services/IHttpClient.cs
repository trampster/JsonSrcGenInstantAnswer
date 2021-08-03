using System.Net.Http;
using System.Threading.Tasks;

namespace JsonSrcGenInstantAnswer.Services
{
   public interface IHttpClient
   {
      Task<HttpResponseMessage> GetAsync(string path);
   }
}