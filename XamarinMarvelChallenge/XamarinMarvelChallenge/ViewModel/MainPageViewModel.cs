using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Taken from Wikipedia
        /// </summary>
        public string MarvelLogo { get; private set; }
        public string CharacterIcon { get; private set; }

        private const string _nameSortByOption = "Name";
        private const string _dateSortByOption = "Date";

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public string[] SortByOptions { get; private set; }

        private ObservableCollection<Character> _searchResults;

        public ObservableCollection<Character> SearchResults
        {
            get { return _searchResults; }
            set { SetProperty(ref _searchResults, value); }
        }

        public Command<string> SearchCharacterCommand { get; private set; }
        public Command<string> SortByCommand { get; private set; }

        public MainPageViewModel()
        {
            Title = "Characters";
            MarvelLogo = "marvel_logo.png";
            CharacterIcon = "characters.png";
            SortByOptions = new string[] { _nameSortByOption, _dateSortByOption };

            SearchCharacterCommand = new Command<string>(GetSearchResults);
            SortByCommand = new Command<string>(SortBy);
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

        public void GetSearchResults(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                SearchResults = GlobalVariables.Characters;
            else
            {
                var searchResultsIEnumerable = GlobalVariables.Characters
                    .Where(x => x.Name.ToLower()
                    .Contains(searchText.ToLower()));
                var newSearchResults = new ObservableCollection<Character>(searchResultsIEnumerable);

                SearchResults = newSearchResults;
            }
        }
    }
}
