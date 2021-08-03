using System.Windows;
using Serilog;
using Serilog.Sinks.File;
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
         var mainWindows = container.Resolve<MainWindow>().Value;
         MainWindow = mainWindows;
         MainWindow.Show();
      }
   }
}
