using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JsonSrcGen;
using JsonSrcGenInstantAnswer.Models;

namespace JsonSrcGenInstantAnswer.Services
{
   public class DuckDuckGoInstantAnswerService : IDuckDuckGoInstantAnswerService
   {
      readonly JsonConverter _jsonConverter;
      readonly InstantAnswer _instantAnswer;

      const string _baseUrl = "https://api.duckduckgo.com";

      public DuckDuckGoInstantAnswerService()
      {
         _jsonConverter = new JsonConverter();
         _instantAnswer = new InstantAnswer();
      }

      public async Task<byte[]> DownloadImage(string path)
      {
         using (HttpClient client = new HttpClient())
         {
            var result = await client.GetAsync($"{_baseUrl}{path}");
            if (result.IsSuccessStatusCode)
            {
               return await result.Content.ReadAsByteArrayAsync();
            }
         }
         return null;
      }

      public async Task<InstantAnswer> Search(string text)
      {
         using (HttpClient client = new HttpClient())
         {
            string querySyntaxText = HttpUtility.UrlEncode(text);
            var result = await client.GetAsync($"{_baseUrl}/?q={querySyntaxText}&format=json");
            if (result.IsSuccessStatusCode)
            {
               var json = await result.Content.ReadAsByteArrayAsync();
               string jsonString = Encoding.UTF8.GetString(json);
               //var systemTextJson = System.Text.Json.JsonSerializer.Deserialize<InstantAnswer>(json);
               _jsonConverter.FromJson(_instantAnswer, json);
               return _instantAnswer;
            }
         }
         return null;
      }
   }
}
