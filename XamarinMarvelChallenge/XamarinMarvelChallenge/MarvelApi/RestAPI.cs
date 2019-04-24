using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using XamarinMarvelChallenge.Extensions;
using XamarinMarvelChallenge.Model.Characters;
using XamarinMarvelChallenge.Model.Comic;
using XamarinMarvelChallenge.Utils;

namespace XamarinMarvelChallenge.MarvelApi
{
    public class RestApi
    {
        private readonly HttpClient _client;
        private const string _publicKey = "f1def8f826359cbe621637efac4cf74c";
        private const string _privateKey = "7fc3dd9c612f602117833595018a48d4b0183d32";

        private string _ts;
        private string _hashString;
        private string _md5Hash;

        public RestApi()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
        }

        public async Task<ObservableCollection<Character>> GetCharacters()
        {
            ObservableCollection<Character> characters;

            SetUpTsHashStringMD5Hash();
            string requestURL = "http://gateway.marvel.com/v1/public/characters"
                + "?apikey=" + _publicKey
                + "&ts=" + _ts 
                + "&hash=" +_md5Hash;
            var url = new Uri(requestURL);

            try
            {
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json;

                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }
                var successfulResponse = JsonConvert.DeserializeObject<CharactersSuccessfulResponse>(json);
                var data = successfulResponse.Data;
                characters = data.Characters;

                foreach (var character in characters)
                {
                    foreach (var comicItem in character.Comics.AssociatedComics)
                    {
                        comicItem.Comic = await GetComic(comicItem.ResourceURI);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                characters = null;
            }

            return characters;
        }

        public async Task<Comic> GetComic(string resourceURI)
        {
            Comic comic;

            SetUpTsHashStringMD5Hash();
            string requestUrl = $"{resourceURI}?apikey={_publicKey}&ts={_ts}&hash={_md5Hash}";
            var url = new Uri(requestUrl);

            try
            {
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json;

                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                var successfulResponse = JsonConvert.DeserializeObject<ComicSuccessfulResponse>(json);
                var data = successfulResponse.Data;
                comic = data.Comics.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                comic = null;
            }

            return comic;
        }

        private void SetUpTsHashStringMD5Hash()
        {
            _ts = DateUtils.GetCurrentTimestampInMiliseconds();
            _hashString = string.Format("{0}{1}{2}", _ts, _privateKey, _publicKey);
            _md5Hash = _hashString.GetMD5Hash();
        }
    }
}
