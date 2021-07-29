
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace JsonSrcGenInstantAnswer.ViewModels
{
   public class SearchViewModel : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      string _searchText = "";
      public string SearchText
      {
         get => _searchText;
         set
         {
            SetProperty(ref _searchText, value);
            UpdateImageColors();
         }
      }

      void UpdateImageColors()
      {
         if(SearchText.Length > 0)
         {
            SearchButtonForegroundColor = _whiteColorBrush;
            SearchButtonBackgroundColor = _yellowColorBrush;
            return;
         }
         SearchButtonForegroundColor = _grayColorBrush;
         SearchButtonBackgroundColor = _transparentColorBrush;
      }

      static readonly SolidColorBrush _grayColorBrush = new SolidColorBrush(Colors.Gray);
      static readonly SolidColorBrush _transparentColorBrush = new SolidColorBrush(Colors.Transparent);
      static readonly SolidColorBrush _yellowColorBrush = new SolidColorBrush(Color.FromRgb(0x63, 0xad, 0x5f));
      static readonly SolidColorBrush _whiteColorBrush = new SolidColorBrush(Colors.White);

      SolidColorBrush _searchButtonForegroundColor = _grayColorBrush;
      public SolidColorBrush SearchButtonForegroundColor
      {
         get => _searchButtonForegroundColor;
         set => SetProperty(ref _searchButtonForegroundColor, value);
      }

      SolidColorBrush _searchButtonBackgroundColor = _transparentColorBrush;
      public SolidColorBrush SearchButtonBackgroundColor
      {
         get => _searchButtonBackgroundColor;
         set => SetProperty(ref _searchButtonBackgroundColor, value);
      }

      void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
      {
         if(backingStore.Equals(value))
         {
            return;
         }
         backingStore = value;
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
