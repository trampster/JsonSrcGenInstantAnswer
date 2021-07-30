using JsonSrcGenInstantAnswer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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

         this.DataContext = searchViewModel;
      }
   }
}
