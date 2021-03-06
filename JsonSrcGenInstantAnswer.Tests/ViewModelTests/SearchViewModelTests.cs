using JsonSrcGenInstantAnswer.Models;
using JsonSrcGenInstantAnswer.Services;
using JsonSrcGenInstantAnswer.ViewModels;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Windows.Media;

namespace JsonSrcGenInstantAnswer.Tests.ViewModelTests
{
   public class SearchViewModelTests
   {
      Mock<IDuckDuckGoInstantAnswerService> _duckDuckGoInstantAnswerServiceMock;
      Mock<IBitmapBuilder> _bitmapBuilderMock;
      SearchViewModel _searchViewModel;

      [SetUp]
      public void Setup()
      {
         _duckDuckGoInstantAnswerServiceMock = new Mock<IDuckDuckGoInstantAnswerService>();
         _bitmapBuilderMock = new Mock<IBitmapBuilder>();
         _searchViewModel = new SearchViewModel(
            _duckDuckGoInstantAnswerServiceMock.Object,
            _bitmapBuilderMock.Object);
      }

      [Test]
      public void SearchButtonForegroundColor_SearchTextNotEmpty_White()
      {
         // arrange
         _searchViewModel.SearchText = "Duck";

         // act
         var searchButtonForegroundColor = _searchViewModel.SearchButtonForegroundColor;

         // assert
         Assert.That(searchButtonForegroundColor.Color, Is.EqualTo(Colors.White));
      }

      [Test]
      public void SearchButtonForegroundColor_SearchTextEmpty_Grey()
      {
         // arrange
         _searchViewModel.SearchText = "";

         // act
         var searchButtonForegroundColor = _searchViewModel.SearchButtonForegroundColor;

         // assert
         Assert.That(searchButtonForegroundColor.Color, Is.EqualTo(Colors.Gray));
      }

      [Test]
      public void SearchButtonBackgroundColor_SearchTextNotEmpty_White()
      {
         // arrange
         _searchViewModel.SearchText = "Duck";

         // act
         var searchButtonBackgroundColor = _searchViewModel.SearchButtonBackgroundColor;

         // assert
         Assert.That(searchButtonBackgroundColor.Color, Is.EqualTo(Color.FromRgb(0x63, 0xad, 0x5f)));
      }

      [Test]
      public void SearchButtonBackgroundColor_SearchTextEmpty_Grey()
      {
         // arrange
         _searchViewModel.SearchText = "";

         // act
         var searchButtonBackgroundColor = _searchViewModel.SearchButtonBackgroundColor;

         // assert
         Assert.That(searchButtonBackgroundColor.Color, Is.EqualTo(Colors.Transparent));
      }

      [Test]
      public async Task Search_NoResult_AbstractUrlEmptyString()
      {
         // arrange
         _duckDuckGoInstantAnswerServiceMock
            .Setup(service => service.Search(It.IsAny<string>()));

         // act
         await _searchViewModel.DoSearch();

         //assert
         Assert.That(_searchViewModel.AbstractUrl, Is.EqualTo(""));
      }

      [Test]
      public async Task Search_NoResult_AbstractSourceEmptyString()
      {
         // arrange
         _duckDuckGoInstantAnswerServiceMock
            .Setup(service => service.Search(It.IsAny<string>()));

         // act
         await _searchViewModel.DoSearch();

         // assert
         Assert.That(_searchViewModel.AbstractSource, Is.EqualTo(""));
      }

      [Test]
      public async Task Search_NoResult_AbstractFailedSearch()
      {
         // arrange
         _duckDuckGoInstantAnswerServiceMock
            .Setup(service => service.Search(It.IsAny<string>()));

         // act
         await _searchViewModel.DoSearch();

         // assert
         Assert.That(_searchViewModel.Abstract, Is.EqualTo(Resources.FailedSearch));
      }

      [Test]
      public async Task Search_NoResult_ImageNull()
      {
         // arrange
         var bitmap = BitmapTestHelpers.CreateBitmapImage();
         _searchViewModel.Image = bitmap;
         _duckDuckGoInstantAnswerServiceMock
            .Setup(service => service.Search(It.IsAny<string>()));

         // act
         await _searchViewModel.DoSearch();

         // assert
         Assert.That(_searchViewModel.Image, Is.Null);
      }


      [Test]
      public async Task Search_GotResult_AbstractUrlSet()
      {
         // arrange
         const string expectedAbstractUrl = "www.mydomain.com";
         _duckDuckGoInstantAnswerServiceMock
            .Setup(service => service.Search(It.IsAny<string>()))
            .ReturnsAsync(new InstantAnswer()
            {
               AbstractURL = expectedAbstractUrl
            });

         // act
         await _searchViewModel.DoSearch();

         // assert
         Assert.That(_searchViewModel.AbstractUrl, Is.EqualTo(expectedAbstractUrl));
      }

      [Test]
      public async Task Search_GotResult_AbstractSourceSet()
      {
         // arrange
         const string expectedAbstractSource = "widipedia";

         _duckDuckGoInstantAnswerServiceMock
            .Setup(service => service.Search(It.IsAny<string>()))
            .ReturnsAsync(new InstantAnswer()
            {
               AbstractSource = expectedAbstractSource
            });

         // act
         await _searchViewModel.DoSearch();

         // assert
         Assert.That(_searchViewModel.AbstractSource, Is.EqualTo(expectedAbstractSource));
      }

      [Test]
      public async Task Search_GotResult_AbstractSet()
      {
         // arrange
         const string expectedAbstract = "Expected abstract text";

         _duckDuckGoInstantAnswerServiceMock
            .Setup(service => service.Search(It.IsAny<string>()))
            .ReturnsAsync(new InstantAnswer()
            {
               Abstract = expectedAbstract
            });

         // act
         await _searchViewModel.DoSearch();

         // assert
         Assert.That(_searchViewModel.Abstract, Is.EqualTo(expectedAbstract));
      }

      [Test]
      public async Task Search_GotResult_SetsImage()
      {
         // arrange
         const string imageUrl = "i/myimage.png";
         var bitmap = BitmapTestHelpers.CreateBitmapImage();
         _searchViewModel.Image = bitmap;
         _duckDuckGoInstantAnswerServiceMock
            .Setup(service => service.Search(It.IsAny<string>()))
            .ReturnsAsync(new InstantAnswer()
            {
               Image = imageUrl
            });

         var expectedImageBytes = new byte[] { 1 };
         _duckDuckGoInstantAnswerServiceMock
            .Setup(service => service.DownloadImage(imageUrl))
            .ReturnsAsync(expectedImageBytes);
         _bitmapBuilderMock
            .Setup(builder => builder.LoadImage(expectedImageBytes))
            .Returns(bitmap);

         // act
         await _searchViewModel.DoSearch();

         // assert
         Assert.That(_searchViewModel.Image, Is.EqualTo(bitmap));
      }

      [Test]
      public void HasSearchText_SearchEmpty_False()
      {
         // arrange
         _searchViewModel.SearchText = "";

         // act
         var hasSearchText = _searchViewModel.HasSearchText;

         // assert
         Assert.That(hasSearchText, Is.False);
      }

      [Test]
      public void HasSearchText_SearchHasText_True()
      {
         // arrange
         _searchViewModel.SearchText = "a";

         // act
         var hasSearchText = _searchViewModel.HasSearchText;

         // assert
         Assert.That(hasSearchText, Is.True);
      }

      [Test]
      public void ClearText_HasText_Cleared()
      {
         // arrange
         _searchViewModel.SearchText = "my search";

         // act
         _searchViewModel.ClearText.Execute(null);

         // assert
         Assert.That(_searchViewModel.SearchText, Is.EqualTo(""));
      }
   }
}
