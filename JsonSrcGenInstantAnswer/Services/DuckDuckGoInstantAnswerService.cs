using JsonSrcGen;
using JsonSrcGenInstantAnswer.Models;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace JsonSrcGenInstantAnswer.Services
{
   public class DuckDuckGoInstantAnswerService : IDuckDuckGoInstantAnswerService
   {
      readonly JsonConverter _jsonConverter;
      readonly InstantAnswer _instantAnswer;
      readonly IHttpClient _httpClient;
      readonly ILogger _logger;

      const string _baseUrl = "https://api.duckduckgo.com";

      public DuckDuckGoInstantAnswerService(
         IHttpClient httpClient, 
         ILogger logger)
      {
         _httpClient = httpClient;
         _logger = logger.ForContext<DuckDuckGoInstantAnswerService>();
         _jsonConverter = new JsonConverter();
         _instantAnswer = new InstantAnswer();
      }

      public async Task<byte[]> DownloadImage(string path)
      {
         try
         {
            var result = await _httpClient.GetAsync($"{_baseUrl}{path}");
            if (result.IsSuccessStatusCode)
            {
               return await result.Content.ReadAsByteArrayAsync();
            }
            return null;
         }
         catch(Exception exception) when (exception is HttpRequestException || exception is TaskCanceledException)
         {
            _logger.Warning($"Failed to Download message with exception {exception.Message}");
            return null;
         }
      }

      public async Task<InstantAnswer> Search(string text)
      {
         try
         {
            string querySyntaxText = HttpUtility.UrlEncode(text);
            var result = await _httpClient.GetAsync($"{_baseUrl}/?q={querySyntaxText}&format=json");
            if (result.IsSuccessStatusCode)
            {
               var json = await result.Content.ReadAsByteArrayAsync();
               _jsonConverter.FromJson(_instantAnswer, json);
               return _instantAnswer;
            }
            return null;
         }
         catch (Exception exception) when (exception is HttpRequestException || exception is TaskCanceledException)
         {
            _logger.Warning($"Failed to search with exception {exception.Message}");
            return null;
         }
      }
   }
}
