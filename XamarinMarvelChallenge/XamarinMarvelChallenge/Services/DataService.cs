using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinMarvelChallenge.Extensions;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.Services
{
    public class DataService
    {
        private ICollection<Character> _characters;

        public DataService()
        {
            Task.Run(() => DownloadCharactersAsync()).Wait();
        }

        private async Task DownloadCharactersAsync()
        {
            _characters = await GlobalVariables.RestApi.GetCharacters();
        }

        public async Task<ObservableCollection<Character>> GetCharactersAsync(int pageIndex, int pageSize)
        {
            await Task.Delay(2000);

            var charactersRange = _characters.Skip(pageIndex * pageSize).Take(pageSize).ToObservableCollection();

            return charactersRange;
        }
    }
}
