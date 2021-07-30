using JsonSrcGenInstantAnswer.ViewModels;
using NUnit.Framework;
using System.Windows.Media;

namespace JsonSrcGenInstantAnswer.Tests.ViewModelTests
{
   public class SearchViewModelTests
   {
      SearchViewModel _searchViewModel;

      [SetUp]
      public void Setup()
      {
         _searchViewModel = new SearchViewModel();
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
   }
}
