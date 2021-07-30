using JsonSrcGenInstantAnswer.ViewModels;
using StrongInject;

namespace JsonSrcGenInstantAnswer
{
   [Register(typeof(SearchViewModel))]
   [Register(typeof(MainWindow))]
   public partial class InstantAnswerContainer : IContainer<MainWindow>
   {
   }
}
