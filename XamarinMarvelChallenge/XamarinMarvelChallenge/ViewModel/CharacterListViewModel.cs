using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using XamarinMarvelChallenge.Extensions;
using XamarinMarvelChallenge.Model;
using XamarinMarvelChallenge.View;

namespace XamarinMarvelChallenge.ViewModel
{
    public class CharacterListViewModel : BaseViewModel
    {
        /// <summary>
        /// This is used to handle MessagingCenter methods
        /// </summary>
        public string SelectCharacterMessageName => "SelectCharacter";

        private string _searchText;

        /// <summary>
        /// It is the 'nameStartsWith' parameter (it can be null or empty)
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private const string _nameOption = "Name";
        private const string _dateOption = "Date";

        public string[] SortByOptions { get; private set; }

        private string _selectedSortByOption;

        /// <summary>
        /// It is the 'orderBy' parameter (it can be null or empty)
        /// </summary>
        public string SelectedSortByOption
        {
            get { return _selectedSortByOption; }
            set { SetProperty(ref _selectedSortByOption, value); }
        }

        private string _loadingMessage;

        /// <summary>
        /// This shows the corresponding message depending on the action performed
        /// </summary>
        public string LoadingMessage
        {
            get { return _loadingMessage; }
            set { SetProperty(ref _loadingMessage, value); }
        }


        /// <summary>
        /// This is the 'limit' parameter (it must not be null)
        /// </summary>
        private int _limit;
        /// <summary>
        /// This is the 'offset' parameter (it can be null)
        /// </summary>
        private int? _offset;

        private InfiniteScrollCollection<Character> __characters;

        /// <summary>
        /// The infinite scroll collection of characters
        /// </summary>
        public InfiniteScrollCollection<Character> Characters
        {
            get { return __characters; }
            set { SetProperty(ref __characters, value); }
        }

        public Page CharacterDetailPage { get; private set; }

        public ICommand SearchCharacterCommand { get; private set; }
        public ICommand SortByCommand { get; private set; }
        public ICommand SelectCharacterCommand { get; private set; }

        #region Flags
        private bool _hasLoadedPage;

        public bool HasLoadedPage
        {
            get { return _hasLoadedPage; }
            set { SetProperty(ref _hasLoadedPage, value); }
        }

        private bool _hasNotLoadedPage;

        public bool HasNotLoadedPage
        {
            get { return _hasNotLoadedPage; }
            private set { SetProperty(ref _hasNotLoadedPage, value); }
        }

        private bool _hasCharacters;

        public bool HasCharacters
        {
            get { return _hasCharacters; }
            set { SetProperty(ref _hasCharacters, value); }
        }

        private bool _doesNotHaveCharacters;

        public bool DoesNotHaveCharacters
        {
            get { return _doesNotHaveCharacters; }
            set { SetProperty(ref _doesNotHaveCharacters, value); }
        }
        #endregion

        public CharacterListViewModel()
        {
            Title = "Characters";

            SearchText = null;
            SelectedSortByOption = null;
            _offset = null;

            HasLoadedPage = false;
            HasNotLoadedPage = true;
            LoadingMessage = "Please wait until the data is retrieved";

            _limit = App.CharacterLimit;

            HasCharacters = false;
            DoesNotHaveCharacters = false;

            SortByOptions = new string[] { _nameOption, _dateOption };

            SearchCharacterCommand = new Command(async () => await GetSearchResults());
            SortByCommand = new Command<string>(async (sortByOption) => await SortBy(sortByOption));
            SelectCharacterCommand = new Command<object>(SelectCharacter);
        }

        /// <summary>
        /// This will handle the selection of a character
        /// </summary>
        /// <param name="obj">The selected character in the UI</param>
        private void SelectCharacter(object obj)
        {
            var selectedCharacter = obj as Character;
            var viewModel = new CharacterDetailViewModel(selectedCharacter);
            CharacterDetailPage = new CharacterDetail(viewModel);

            MessagingCenter.Send(this, SelectCharacterMessageName);
        }

        #region Character calls and filters
        private async Task<ObservableCollection<Character>> GetCharactersAsync()
        {
            var characters = await App.RestApiObject.GetCharactersAsync(_limit, SearchText, SelectedSortByOption, _offset);

            return characters;
        }

        /// <summary>
        /// This will be called once in order the initialize the data when the page is built
        /// </summary>
        /// <returns>The task that queries the api</returns>
        public async Task InitializeDataAsync()
        {
            await SetupInfiniteScrollCollection();

            HasLoadedPage = true;
            HasNotLoadedPage = false;
        }

        /// <summary>
        /// This loads the following results of the infinite scroll collection
        /// </summary>
        /// <returns>The task that queries the api</returns>
        private async Task<ObservableCollection<Character>> LoadMoreCharactersAsync()
        {
            if (_limit != App.CharacterLimit)
                _limit = App.CharacterLimit;

            IsBusy = true;
            _offset = (Characters.Count / _limit) * _limit;
            ObservableCollection<Character> items = await GetCharactersAsync();
            IsBusy = false;
            return items;
        }

        /// <summary>
        /// This sorts the data by name or date modified
        /// </summary>
        /// <param name="sortByOption">This is the sorting option. It only can be by name or by date modified</param>
        /// <returns>The task that queries the api</returns>
        private async Task SortBy(string sortByOption)
        {
            switch (sortByOption)
            {
                case _nameOption:
                    SelectedSortByOption = "name";
                    break;
                case _dateOption:
                    SelectedSortByOption = "modified";
                    break;
            }

            if (_limit != Characters.Count)
                _limit = Characters.Count;

            if (_offset == null)
                _offset = (Characters.Count / _limit) * _limit;

            HasLoadedPage = false;
            HasNotLoadedPage = true;
            LoadingMessage = "Please wait until the data is reordered";

            await SetupInfiniteScrollCollection();

            HasLoadedPage = true;
            HasNotLoadedPage = false;

        }

        /// <summary>
        /// This will search by 'nameStartsWith' parameter
        /// </summary>
        /// <returns>The task that queries the api</returns>
        private async Task GetSearchResults()
        {
            _offset = null;

            HasLoadedPage = false;
            HasNotLoadedPage = true;
            LoadingMessage = "Please wait until the data is retrieved";

            await SetupInfiniteScrollCollection();

            HasLoadedPage = true;
            HasNotLoadedPage = false;
        }

        /// <summary>
        /// This will set the OnLoadMore and OnCanLoadMore
        /// </summary>
        private async Task SetupInfiniteScrollCollection()
        {
            var characters = await GetCharactersAsync();
            Characters = characters.ToInfiniteScrollCollection();
            Characters.OnLoadMore = async () => await LoadMoreCharactersAsync();
            Characters.OnCanLoadMore = () => Characters.Count <= App.MaxCharacters;
            HasCharacters = Characters.Count > 0;
            DoesNotHaveCharacters = Characters.Count == 0;
        }
        #endregion
    }
}
