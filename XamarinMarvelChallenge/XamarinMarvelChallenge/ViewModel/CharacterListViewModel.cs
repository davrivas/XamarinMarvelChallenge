using MvvmHelpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using XamarinMarvelChallenge.Extensions;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model;
using XamarinMarvelChallenge.View;

namespace XamarinMarvelChallenge.ViewModel
{
    public class CharacterListViewModel : BaseViewModel
    {
        public string SelectCharacterMessageName => "SelectCharacter";

        public const string NameSortByOption = "Name";
        public const string DateSortByOption = "Modified";
        public string SelectedSortByOption { get; set; }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public string[] SortByOptions { get; private set; }

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
            Title = "Characters";

            Characters = new InfiniteScrollCollection<Character>
            {
                OnLoadMore = async () => await LoadMoreCharactersAsync(),
                OnCanLoadMore = () => Characters.Count <= 100 // until the end I think so
            };
            Task.Run(() => DownloadDataAsync()).Wait();

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
        private async Task DownloadDataAsync()
        {
            ObservableCollection<Character> items = await GlobalVariables.RestApi.GetCharactersAsync();
            Characters.AddRange(items);
        }

        private async Task<IEnumerable<Character>> LoadMoreCharactersAsync()
        {
            IsBusy = true;
            int page = Characters.Count / GlobalVariables.CharacterLimit;
            ObservableCollection<Character> items = await GlobalVariables.RestApi.GetCharactersAsync(
                nameStartsWith: SearchText,
                orderBy: SelectedSortByOption,
                offset: page);
            IsBusy = false;
            return items;
        }

        public async Task<ObservableCollection<Character>> GetCharactersAsync(int pageIndex, int pageSize)
        {
            await Task.Delay(2000);

            var charactersRange = SearchResults.Skip(pageIndex * pageSize).Take(pageSize).ToObservableCollection();

            return charactersRange;
        }

        private async Task SortBy(string sortByOption)
        {
            switch (sortByOption)
            {
                case NameSortByOption:
                    SelectedSortByOption = "name";;
                    break;
                case DateSortByOption:
                    SelectedSortByOption = "modified";
                    break;
            }

            Characters = _dataService.SortBy(sortByOption).ToInfiniteScrollCollection();
            Characters.OnLoadMore = async () => await LoadMoreCharactersAsync();
            Characters.OnCanLoadMore = () => Characters.Count <= _dataService.SearchResults.Count;
        }

        private async Task GetSearchResults()
        {
            
            Characters = _dataService.GetSearchResults(SearchText).ToInfiniteScrollCollection();
            Characters.OnLoadMore = async () => await LoadMoreCharactersAsync();
            Characters.OnCanLoadMore = () => Characters.Count <= _dataService.SearchResults.Count;
        }
        #endregion
    }
}
