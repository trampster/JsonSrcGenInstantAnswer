using JsonSrcGenInstantAnswer.Services;
using JsonSrcGenInstantAnswer.ViewModels;
using Serilog;
using StrongInject;

namespace JsonSrcGenInstantAnswer
{
   [Register(typeof(DuckDuckGoInstantAnswerService), typeof(IDuckDuckGoInstantAnswerService))]
   [Register(typeof(BitmapBuilder), typeof(IBitmapBuilder))]
   [Register(typeof(HttpClientWrapper), typeof(IHttpClient))]
   [Register(typeof(SearchViewModel))]
   [Register(typeof(MainWindow))]
   public partial class InstantAnswerContainer : IContainer<MainWindow>
   {
      [Factory(Scope.SingleInstance)]
      public ILogger CreateLogger()
      {
         return new LoggerConfiguration()
            .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
      }
   }
}
