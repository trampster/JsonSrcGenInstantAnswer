﻿using JsonSrcGenInstantAnswer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace JsonSrcGenInstantAnswer
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow(SearchViewModel searchViewModel)
      {
         InitializeComponent();

         DataContext = searchViewModel;
      }

      void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
      {
         // for .NET Core you need to add UseShellExecute = true
         // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
         var processStartInfo = new ProcessStartInfo(e.Uri.AbsoluteUri);
         processStartInfo.UseShellExecute = true;
         Process.Start(processStartInfo);
         e.Handled = true;
      }
   }
}
