using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinMarvelChallenge.Extensions;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.Services
{
    public class CharacterDataService
    {
        public ObservableCollection<Character> Characters { get; set; }
        public ObservableCollection<Character> SearchResults { get; set; }

        public CharacterDataService()
        {
            Task.Run(() => DownloadCharactersAsync()).Wait();
            SearchResults = Characters;
        }

        private async Task DownloadCharactersAsync()
        {
            Characters = await GlobalVariables.RestApi.GetCharacters();
        }

        public async Task<ObservableCollection<Character>> GetCharactersAsync(int pageIndex, int pageSize)
        {
            await Task.Delay(2000);

            var charactersRange = SearchResults.Skip(pageIndex * pageSize).Take(pageSize).ToObservableCollection();

            return charactersRange;
        }

        public ObservableCollection<Character> GetSearchResults(string query)
        {
            ObservableCollection<Character> searchResults;

            if (string.IsNullOrWhiteSpace(query))
            {
                SearchResults = Characters;
                searchResults = SearchResults;
            }
            else
            {
                SearchResults = Characters
                    .Where(x => x.Name.ToLower()
                    .Contains(query.ToLower()))
                    .ToObservableCollection();
                searchResults = SearchResults;
            }

            return searchResults;
        }

        public ObservableCollection<Character> SortBy(string sortByOption)
        {
            ObservableCollection<Character> searchResult = null;

            switch (sortByOption)
            {
                case CharacterListViewModel.NameSortByOption:
                    searchResult = SearchResults
                        .OrderBy(x => x.Name)
                        .ToObservableCollection();
                    break;
                case CharacterListViewModel.DateSortByOption:
                    searchResult = SearchResults
                        .OrderBy(x => x.Modified)
                        .ToObservableCollection();
                    break;
            }

            return searchResult;
        }
    }
}
