using JsonSrcGenInstantAnswer.Services;
using JsonSrcGenInstantAnswer.ViewModels;
using StrongInject;

namespace JsonSrcGenInstantAnswer
{
   [Register(typeof(DuckDuckGoInstantAnswerService), typeof(IDuckDuckGoInstantAnswerService))]
   [Register(typeof(BitmapBuilder), typeof(IBitmapBuilder))]
   [Register(typeof(SearchViewModel))]
   [Register(typeof(MainWindow))]
   public partial class InstantAnswerContainer : IContainer<MainWindow>
   {
   }
}
