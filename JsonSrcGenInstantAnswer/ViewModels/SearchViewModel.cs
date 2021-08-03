
using JsonSrcGenInstantAnswer.Services;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JsonSrcGenInstantAnswer.ViewModels
{
   public class SearchViewModel : INotifyPropertyChanged
   {
      readonly IDuckDuckGoInstantAnswerService _duckDuckGoInstantAnswerService;
      readonly IBitmapBuilder _bitmapBuilder;

      public event PropertyChangedEventHandler PropertyChanged;

      public SearchViewModel(
         IDuckDuckGoInstantAnswerService duckDuckGoInstantAnswerService,
         IBitmapBuilder bitmapBuilder)
      {
         _duckDuckGoInstantAnswerService = duckDuckGoInstantAnswerService;
         _bitmapBuilder = bitmapBuilder;
      }

      string _searchText = "";
      public string SearchText
      {
         get => _searchText;
         set
         {
            SetProperty(ref _searchText, value);
            HasSearchText = _searchText.Length > 0;
            UpdateImageColors();
         }
      }

      
      bool _hasSearchText = false;
      public bool HasSearchText
      {
         get => _hasSearchText;
         set
         {
            SetProperty(ref _hasSearchText, value);
         }
      }

      void UpdateImageColors()
      {
         if(SearchText.Length > 0)
         {
            SearchButtonForegroundColor = _whiteColorBrush;
            SearchButtonBackgroundColor = _greenColorBrush;
            return;
         }
         SearchButtonForegroundColor = _grayColorBrush;
         SearchButtonBackgroundColor = _transparentColorBrush;
      }

      static readonly SolidColorBrush _grayColorBrush = new SolidColorBrush(Colors.Gray);
      static readonly SolidColorBrush _transparentColorBrush = new SolidColorBrush(Colors.Transparent);
      static readonly SolidColorBrush _greenColorBrush = new SolidColorBrush(Color.FromRgb(0x63, 0xad, 0x5f));
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
         if(backingStore != null && backingStore.Equals(value))
         {
            return;
         }
         backingStore = value;
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      public ICommand ClearText => new AsyncCommand(DoClearText);

      Task DoClearText()
      {
         SearchText = "";
         return Task.CompletedTask;
      }

      public ICommand Search => new AsyncCommand(DoSearch);

      public async Task DoSearch()
      {
         var answer = await _duckDuckGoInstantAnswerService.Search(SearchText);
         if(answer == null)
         {
            AbstractUrl = "";
            Abstract = Resources.FailedSearch;
            AbstractSource = "";
            Image = null;
            return;
         }
         
         Abstract = string.IsNullOrEmpty(answer.Abstract) ? Resources.NoInformationFound : answer.Abstract;
         AbstractUrl = answer.AbstractURL;
         AbstractSource = answer.AbstractSource;

         if (string.IsNullOrEmpty(answer.Image))
         {
            Image = null;
         }
         else
         {
            var imageBytes = await _duckDuckGoInstantAnswerService.DownloadImage(answer.Image);
            Image = _bitmapBuilder.LoadImage(imageBytes);
         }
      }

      string _abstractUrl = "";
      public string AbstractUrl
      {
         get => _abstractUrl;
         set => SetProperty(ref _abstractUrl, value);
      }

      string _abstractSource = "";
      public string AbstractSource
      {
         get => _abstractSource;
         set => SetProperty(ref _abstractSource, value);
      }

      int _imageWidth = 0;
      public int ImageWidth
      {
         get => _imageWidth;
         set => SetProperty(ref _imageWidth, value);
      }

      static BitmapImage LoadImage(byte[] imageData)
      {
         if (imageData == null || imageData.Length == 0) return null;
         var image = new BitmapImage();
         using (var mem = new MemoryStream(imageData))
         {
            mem.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = mem;
            image.EndInit();
         }
         image.Freeze();
         return image;
      }

      string _abstract = "";
      public string Abstract
      {
         get => _abstract;
         set => SetProperty(ref _abstract, value);
      }

      BitmapImage _image;
      public BitmapImage Image
      {
         get => _image;
         set
         {
            if (value == null)
            {
               ImageWidth = 0;
            }
            else if(value.Width > 300)
            {
               ImageWidth = 300;
            }
            else
            {
               ImageWidth = (int)value.Width;
            }
            SetProperty(ref _image, value);
         }
      }
   }
}
