using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JsonSrcGenInstantAnswer.Services
{
   public class HttpClientWrapper : IHttpClient, IDisposable
   {
      readonly HttpClient _client = new HttpClient();
      bool _disposedValue;

      public Task<HttpResponseMessage> GetAsync(string path) => _client.GetAsync(path);

      protected virtual void Dispose(bool disposing)
      {
         if (!_disposedValue)
         {
            if (disposing)
            {
               _client.Dispose();
            }

            _disposedValue = true;
         }
      }

      public void Dispose()
      {
         Dispose(disposing: true);
         GC.SuppressFinalize(this);
      }
   }
}
