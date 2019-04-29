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
            set { SetProperty(ref _searchText, value); }
        }

        public const string NameSortByOption = "Name";
        public const string DateSortByOption = "Modified";
        public string[] SortByOptions { get; private set; }
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

        public CharacterListViewModel()
        {
            Task.Run(() => DownloadDataAsync()).Wait();

            Title = "Characters";

            SearchText = null;
            SelectedSortByOption = null;
            _offset = null;

            SortByOptions = new string[] { NameSortByOption, DateSortByOption };

            SearchCharacterCommand = new Command(async () => await GetSearchResults());
            SortByCommand = new Command<string>(async (sortByOption) => await SortBy(sortByOption));
            SelectCharacterCommand = new Command<object>(SelectCharacter);
        }

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

        private async Task DownloadDataAsync()
        {
            _limit = App.CharacterLimit;
            ObservableCollection<Character> items = await GetCharactersAsync();
            Characters = new InfiniteScrollCollection<Character>();
            SetupInfiniteScrollCollection();
            Characters.AddRange(items);
        }

        private async Task<IEnumerable<Character>> LoadMoreCharactersAsync()
        {
            IsBusy = true;
            _offset = (Characters.Count / _limit) * _limit;
            ObservableCollection<Character> items = await GetCharactersAsync();
            IsBusy = false;
            return items;
        }

        public async Task<ObservableCollection<Character>> GetCharactersByRangeAsync(int pageIndex, int pageSize)
        {
            await Task.Delay(2000);

            var characters = await GetCharactersAsync();
            var charactersRange = characters.Skip(pageIndex * pageSize).Take(pageSize).ToObservableCollection();

            return charactersRange;
        }

        private async Task SortBy(string sortByOption)
        {
            switch (sortByOption)
            {
                case NameSortByOption:
                    SelectedSortByOption = "name";
                    break;
                case DateSortByOption:
                    SelectedSortByOption = "modified";
                    break;
            }

            _limit = Characters.Count;
            _offset = 0;
            var characters = await GetCharactersAsync();
            Characters = characters.ToInfiniteScrollCollection();
            SetupInfiniteScrollCollection();

        }

        private async Task GetSearchResults()
        {
            _limit = App.CharacterLimit;
            _offset = 0;
            var characters = await GetCharactersAsync();
            Characters = characters.ToInfiniteScrollCollection();
            SetupInfiniteScrollCollection();
        }

        private void SetupInfiniteScrollCollection()
        {
            Characters.OnLoadMore = async () => await LoadMoreCharactersAsync();
            Characters.OnCanLoadMore = () => Characters.Count <= App.MaxCharacters;
        }
        #endregion
    }
}
