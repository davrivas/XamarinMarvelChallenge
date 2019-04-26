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
using XamarinMarvelChallenge.Services;
using XamarinMarvelChallenge.View;

namespace XamarinMarvelChallenge.ViewModel
{
    public class CharacterListViewModel : BaseViewModel
    {
        public string SelectCharacterMessageName => "SelectCharacter";
        private const int _pageSize = 5;
        private readonly CharacterDataService _dataService;
        public const string NameSortByOption = "Name";
        public const string DateSortByOption = "Date";

        // If it is 0 is not ordered, if it is -1 is descending and 1 is ascending
        public int NameOrder { get; set; }
        public int DateOrder { get; set; }
        public int? Offset { get; set; }

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
            _dataService = new CharacterDataService();
            Title = "Characters";

            Characters = new InfiniteScrollCollection<Character>
            {
                OnLoadMore = async () => await LoadMoreCharactersAsync(),
                OnCanLoadMore = () => Characters.Count <= _dataService.SearchResults.Count
            };
            Task.Run(() => DownloadDataAsync()).Wait();

            SortByOptions = new string[] { NameSortByOption, DateSortByOption };
            NameOrder = 0;
            DateOrder = 0;
            Offset = null;

            SearchCharacterCommand = new Command(GetSearchResults);
            SortByCommand = new Command<string>(SortBy);
            SelectCharacterCommand = new Command<object>(SelectCharacter);
        }

        private async Task DownloadDataAsync()
        {
            ObservableCollection<Character> items = await _dataService.GetCharactersAsync(0, GlobalVariables.CharacterLimit);
            Characters.AddRange(items);
        }

        private async Task<IEnumerable<Character>> LoadMoreCharactersAsync()
        {
            IsBusy = true;
            int page = Characters.Count / _pageSize;
            ObservableCollection<Character> items = await _dataService.GetCharactersAsync(page, GlobalVariables.CharacterLimit);
            IsBusy = false;
            return items;
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
            Characters = _dataService.SortBy(sortByOption).ToInfiniteScrollCollection();
            Characters.OnLoadMore = async () => await LoadMoreCharactersAsync();
            Characters.OnCanLoadMore = () => Characters.Count <= _dataService.SearchResults.Count;
        }

        private void GetSearchResults()
        {
            Characters = _dataService.GetSearchResults(SearchText).ToInfiniteScrollCollection();
            Characters.OnLoadMore = async () => await LoadMoreCharactersAsync();
            Characters.OnCanLoadMore = () => Characters.Count <= _dataService.SearchResults.Count;
        }

        //private string GetOrderBy()
        //{
        //    if (NameOrder == 0 && DateOrder == 0)
        //        return null;

        //    string orderBy;

        //    if (NameOrder == 1)
        //    {
        //        if (DateOrder == 1)
        //        {
        //            //orderBy = 
        //        }
        //    }

        //    return orderBy;
        //}
    }
}
