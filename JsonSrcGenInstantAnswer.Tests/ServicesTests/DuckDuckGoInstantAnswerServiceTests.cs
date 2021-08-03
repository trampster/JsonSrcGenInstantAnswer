using JsonSrcGenInstantAnswer.Models;
using JsonSrcGenInstantAnswer.Services;
using Moq;
using NUnit.Framework;
using Serilog;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonSrcGenInstantAnswer.Tests.ServicesTests
{
   public class DuckDuckGoInstantAnswerServiceTests
   {
      Mock<IHttpClient> _httpClientMock;
      Mock<ILogger> _loggerMock;
      DuckDuckGoInstantAnswerService _duckDuckGoInstantAnswerService;
      const string _expectedBasePath = "https://api.duckduckgo.com";

      [SetUp]
      public void Setup()
      {
         _httpClientMock = new Mock<IHttpClient>();
         _loggerMock = new Mock<ILogger>();
         _loggerMock
            .Setup(logger => logger.ForContext<DuckDuckGoInstantAnswerService>())
            .Returns(_loggerMock.Object);
         _duckDuckGoInstantAnswerService = new DuckDuckGoInstantAnswerService(
            _httpClientMock.Object,
            _loggerMock.Object);
      }

      [TestCase(HttpStatusCode.BadRequest)]
      [TestCase(HttpStatusCode.NotFound)]
      [TestCase(HttpStatusCode.InternalServerError)]
      [TestCase(HttpStatusCode.Conflict)]
      [TestCase(HttpStatusCode.BadGateway)]
      [TestCase(HttpStatusCode.Forbidden)]
      public async Task DownloadImage_UnsuccessfulStatus_ReturnsNull(HttpStatusCode statusCode)
      {

         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/mypath"))
            .ReturnsAsync(new HttpResponseMessage()
            {
               StatusCode = statusCode
            });

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.DownloadImage("/mypath");

         // assert
         Assert.That(imageBytes, Is.Null);
      }
      
      [Test]
      public async Task DownloadImage_HttpRequestException_ReturnsNull()
      {
         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/mypath"))
            .Throws(new HttpRequestException());

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.DownloadImage("/mypath");

         // assert
         Assert.That(imageBytes, Is.Null);
      }

      [Test]
      public async Task DownloadImage_HttpRequestException_LogsWarning()
      {
         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/mypath"))
            .Throws(new HttpRequestException());

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.DownloadImage("/mypath");

         // assert
         _loggerMock
            .Verify(
               logger => logger.Warning(It.Is<string>(message => message.StartsWith("Failed to Download message with exception"))),
               Times.Once());
      }
      
      [Test]
      public async Task DownloadImage_TaskCanceledException_ReturnsNull()
      {
         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/mypath"))
            .Throws(new TaskCanceledException());

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.DownloadImage("/mypath");

         // assert
         Assert.That(imageBytes, Is.Null);
      }

      [Test]
      public async Task DownloadImage_TaskCanceledException_LogsWarning()
      {
         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/mypath"))
            .Throws(new TaskCanceledException());

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.DownloadImage("/mypath");

         // assert
         _loggerMock
            .Verify(
               logger => logger.Warning(It.Is<string>(message => message.StartsWith("Failed to Download message with exception"))),
               Times.Once());
      }

      [Test]
      public async Task DownloadImage_Success_ReturnsBytes()
      {
         // arrange
         byte[] expectedImageBytes = new byte[] { 1, 2, 3 };
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/mypath"))
            .ReturnsAsync(new HttpResponseMessage()
            {
               Content = new ByteArrayContent(expectedImageBytes)
            });

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.DownloadImage("/mypath");

         // assert
         Assert.That(imageBytes, Is.EqualTo(expectedImageBytes));
      }

      //////////////////////

      [TestCase(HttpStatusCode.BadRequest)]
      [TestCase(HttpStatusCode.NotFound)]
      [TestCase(HttpStatusCode.InternalServerError)]
      [TestCase(HttpStatusCode.Conflict)]
      [TestCase(HttpStatusCode.BadGateway)]
      [TestCase(HttpStatusCode.Forbidden)]
      public async Task Search_UnsuccessfulStatus_ReturnsNull(HttpStatusCode statusCode)
      {

         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/?q=searchtext&format=json"))
            .ReturnsAsync(new HttpResponseMessage()
            {
               StatusCode = statusCode
            });

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.Search("searchtext");

         // assert
         Assert.That(imageBytes, Is.Null);
      }

      [Test]
      public async Task Search_HttpRequestException_ReturnsNull()
      {
         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/?q=searchtext&format=json"))
            .Throws(new HttpRequestException());

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.Search("searchtext");

         // assert
         Assert.That(imageBytes, Is.Null);
      }

      [Test]
      public async Task Search_HttpRequestException_LogsWarning()
      {
         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/?q=searchtext&format=json"))
            .Throws(new HttpRequestException());

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.Search("searchtext");

         // assert
         _loggerMock
            .Verify(
               logger => logger.Warning(It.Is<string>(message => message.StartsWith("Failed to search with exception"))),
               Times.Once());
      }

      [Test]
      public async Task Search_TaskCanceledException_ReturnsNull()
      {
         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/?q=searchtext&format=json"))
            .Throws(new TaskCanceledException());

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.Search("searchtext");

         // assert
         Assert.That(imageBytes, Is.Null);
      }

      [Test]
      public async Task Search_TaskCanceledException_LogsWarning()
      {
         // arrange
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/?q=searchtext&format=json"))
            .Throws(new TaskCanceledException());

         // act
         var imageBytes = await _duckDuckGoInstantAnswerService.Search("searchtext");

         // assert
         _loggerMock
            .Verify(
               logger => logger.Warning(It.Is<string>(message => message.StartsWith("Failed to search with exception"))),
               Times.Once());
      }

      [Test]
      public async Task Search_Success_ReturnsInstantAnswser()
      {
         // arrange
         var expectedInstantAnswer = new InstantAnswer()
         {
            Abstract = "My Abstract"
         };
         byte[] expectedJsonBytes = JsonSerializer.SerializeToUtf8Bytes(expectedInstantAnswer);
         _httpClientMock
            .Setup(httpClient => httpClient.GetAsync($"{_expectedBasePath}/?q=searchtext&format=json"))
            .ReturnsAsync(new HttpResponseMessage()
            {
               Content = new ByteArrayContent(expectedJsonBytes)
            });

         // act
         var instantAnswser = await _duckDuckGoInstantAnswerService.Search("searchtext");

         // assert
         Assert.That(instantAnswser.Abstract, Is.EqualTo(expectedInstantAnswer.Abstract));
      }

   }
}
