using JsonSrcGenInstantAnswer.ViewModels;
using System.Windows;

namespace JsonSrcGenInstantAnswer
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         MainWindow = new MainWindow(new SearchViewModel());
         MainWindow.Show();
      }
   }
}
