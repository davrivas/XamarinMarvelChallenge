using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model.Characters;
using XamarinMarvelChallenge.View;

namespace XamarinMarvelChallenge.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private const string _nameSortByOption = "Name";
        private const string _dateSortByOption = "Date";

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public string[] SortByOptions { get; private set; }

        private ICollection<Character> _searchResults;

        public ICollection<Character> SearchResults
        {
            get { return _searchResults; }
            set { SetProperty(ref _searchResults, value); }
        }

        public ICommand SearchCharacterCommand { get; private set; }
        public ICommand SortByCommand { get; private set; }
        public ICommand SelectComicCommand { get; private set; }

        public MainPageViewModel()
        {
            SortByOptions = new string[] { _nameSortByOption, _dateSortByOption };

            SearchCharacterCommand = new Command(GetSearchResults);
            SortByCommand = new Command<string>(SortBy);
            SelectComicCommand = new Command<object>(async vm => await SelectComic(vm));
        }

        private async Task SelectComic(object obj)
        {
            var selectedComicItem = obj as ComicsItem;
            var viewModel = new ComicDetailViewModel(selectedComicItem);
            var page = new ComicDetail(viewModel);

            await GlobalVariables.CurrentPage.Navigation.PushAsync(page);
        }

        private void SortBy(string sortByOption)
        {
            IEnumerable<Character> searchResultsIEnumerable = null;

            switch (sortByOption)
            {
                case _nameSortByOption:
                    searchResultsIEnumerable = SearchResults
                        .OrderBy(x => x.Name);
                    break;
                case _dateSortByOption:
                    searchResultsIEnumerable = SearchResults
                        .OrderBy(x => x.Modified);
                    break;
            }

            var orderedResults = new ObservableCollection<Character>(searchResultsIEnumerable);
            SearchResults = orderedResults;
        }

        public void GetSearchResults()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                SearchResults = GlobalVariables.Characters;
            else
            {
                var searchResultsIEnumerable = GlobalVariables.Characters
                    .Where(x => x.Name.ToLower()
                    .Contains(SearchText.ToLower()));
                var newSearchResults = new ObservableCollection<Character>(searchResultsIEnumerable);

                SearchResults = newSearchResults;
            }
        }
    }
}
