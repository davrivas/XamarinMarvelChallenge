using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinMarvelChallenge.Extensions;
using XamarinMarvelChallenge.Model;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.Services
{
    public class CharacterDataService
    {
        private ObservableCollection<Character> _characters;
        public ObservableCollection<Character> SearchResults { get; set; }

        public CharacterDataService()
        {
            //_characters = GlobalVariables.Characters;
            SearchResults = _characters;
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
                SearchResults = _characters;
                searchResults = SearchResults;
            }
            else
            {
                SearchResults = _characters
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
