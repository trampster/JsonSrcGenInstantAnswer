using System.Windows;
using StrongInject;

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

         var container = new InstantAnswerContainer();
         container.Run<MainWindow>(mainWindows =>
         {
            MainWindow = mainWindows;
            MainWindow.Show();
         });
      }
   }
}
