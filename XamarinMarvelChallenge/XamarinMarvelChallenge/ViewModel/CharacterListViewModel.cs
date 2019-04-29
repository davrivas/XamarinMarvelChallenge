using MvvmHelpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            set
            {
                SetProperty(ref _searchText, value);
                
            }
        }

        public string NameSortByOption => _isNameOrderAsc ? "Name (asc)" : "Name (desc)";
        public string DateSortByOption => _isDateOrderAsc ? "Date (asc)" : "Date (desc)";

        private ObservableCollection<string> _sortByOptions;

        public ObservableCollection<string> SortByOptions
        {
            get { return _sortByOptions; }
            set { SetProperty(ref _sortByOptions, value); }
        }
        /// <summary>
        /// It is the 'orderBy' parameter (it can be null or empty)
        /// </summary>
        public string SelectedSortByOption { get; set; }

        /// <summary>
        /// This is the 'limit' parameter (it must not be null)
        /// </summary>
        private int _limit;
        /// <summary>
        /// This is the offset parameter (it can be null)
        /// </summary>
        private int? _offset;

        private InfiniteScrollCollection<Character> __characters;

        public InfiniteScrollCollection<Character> Characters
        {
            get { return __characters; }
            set { SetProperty(ref __characters, value); }
        }

        public Page CharacterDetailPage { get; private set; }

        public ICommand SearchCharacterCommand { get; private set; }
        public ICommand SortByCommand { get; private set; }
        public ICommand SelectCharacterCommand { get; private set; }

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

        public bool HasCharacters => Characters.Count > 0;
        public bool DoesNotHaveCharacters => Characters.Count == 0;
        private bool _isNameOrderAsc;
        private bool _isDateOrderAsc;

        public CharacterListViewModel()
        {
            Title = "Characters";

            SearchText = null;
            SelectedSortByOption = null;
            _offset = null;

            _isNameOrderAsc = true;
            _isDateOrderAsc = true;
            SortByOptions = new ObservableCollection<string> { NameSortByOption, DateSortByOption };

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
            HasLoadedPage = false;
            HasNotLoadedPage = true;
            _limit = App.CharacterLimit;
            ObservableCollection<Character> items = await GetCharactersAsync();
            Characters = new InfiniteScrollCollection<Character>();
            SetupInfiniteScrollCollection();
            Characters.AddRange(items);
            HasLoadedPage = true;
            HasNotLoadedPage = false;
        }

        /// <summary>
        /// This loads the following results
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
                case "Name (asc)":
                case "Name (desc)":
                    if (!_isDateOrderAsc)
                    {
                        _isDateOrderAsc = true;
                        OnPropertyChanged(nameof(DateSortByOption));
                    }
                        

                    if (_isNameOrderAsc)
                    {
                        SelectedSortByOption = "name";
                        _isNameOrderAsc = false;
                    }
                    else
                    {
                        SelectedSortByOption = "-name";
                        _isNameOrderAsc = true;
                    }

                    OnPropertyChanged(nameof(NameSortByOption));
                    break;
                case "Date (asc)":
                case "Date (desc)":
                    if (!_isNameOrderAsc)
                    {
                        _isNameOrderAsc = true;
                        OnPropertyChanged(nameof(NameSortByOption));
                    }
                    
                    if (_isDateOrderAsc)
                    {
                        SelectedSortByOption = "modified";
                        _isDateOrderAsc = false;
                    }
                    else
                    {
                        SelectedSortByOption = "-modified";
                        _isDateOrderAsc = true;
                    }

                    OnPropertyChanged(nameof(DateSortByOption));
                    break;
            }

            OnPropertyChanged(nameof(SortByOptions));

            if (_limit != Characters.Count)
                _limit = Characters.Count;

            if (_offset == null)
                _offset = (Characters.Count / _limit) * _limit;            

            HasLoadedPage = false;
            HasNotLoadedPage = true;

            var characters = await GetCharactersAsync();
            Characters = characters.ToInfiniteScrollCollection();
            SetupInfiniteScrollCollection();

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

            var characters = await GetCharactersAsync();
            Characters = characters.ToInfiniteScrollCollection();
            SetupInfiniteScrollCollection();

            HasLoadedPage = true;
            HasNotLoadedPage = false;
        }

        /// <summary>
        /// This will set the OnLoadMore and OnCanLoadMore
        /// </summary>
        private void SetupInfiniteScrollCollection()
        {
            Characters.OnLoadMore = async () => await LoadMoreCharactersAsync();
            Characters.OnCanLoadMore = () => Characters.Count <= App.MaxCharacters;
            OnPropertyChanged(nameof(HasCharacters));
            OnPropertyChanged(nameof(DoesNotHaveCharacters));
        }
        #endregion
    }
}
