using MvvmHelpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMarvelChallenge.Extensions;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model;
using XamarinMarvelChallenge.View;

namespace XamarinMarvelChallenge.ViewModel
{
    public class CharacterListViewModel : BaseViewModel
    {
        public string SelectCharacterMessageName => "SelectCharacter";
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
            set
            {
                SetProperty(ref _searchResults, value);
                OnPropertyChanged(nameof(HasCharacters));
                OnPropertyChanged(nameof(DoesNotHaveCharacters));
            }
        }

        public Page CharacterDetailPage { get; private set; }

        public ICommand SearchCharacterCommand { get; private set; }
        public ICommand SortByCommand { get; private set; }
        public ICommand SelectCharacterCommand { get; private set; }

        public bool HasCharacters => SearchResults.Count > 0;
        public bool DoesNotHaveCharacters => SearchResults.Count == 0;

        public CharacterListViewModel()
        {
            Title = "Characters";
            SearchResults = GlobalVariables.Characters == null ? new ObservableCollection<Character>() : GlobalVariables.Characters;
            IsBusy = GlobalVariables.Characters == null;
            IsNotBusy = GlobalVariables.Characters != null;
            SortByOptions = new string[] { _nameSortByOption, _dateSortByOption };
            SearchCharacterCommand = new Command(GetSearchResults);
            SortByCommand = new Command<string>(SortBy);
            SelectCharacterCommand = new Command<object>(SelectCharacter);
        }

        private void SelectCharacter(object obj)
        {
            var selectedCharacter = obj as Character;
            var viewModel = new CharacterDetailViewModel(selectedCharacter);
            CharacterDetailPage = new CharacterDetail(viewModel);

            MessagingCenter.Send(this, SelectCharacterMessageName);
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

            var orderedResults = searchResultsIEnumerable.ToObservableCollection();
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
