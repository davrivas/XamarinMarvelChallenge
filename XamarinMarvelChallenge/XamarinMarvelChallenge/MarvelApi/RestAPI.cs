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
        private string _requestUrl;

        private string _jsonString;
        private HttpResponseMessage _response;

        public RestApi()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
        }

        public async Task<ObservableCollection<Character>> GetCharacters()
        {
            ObservableCollection<Character> characters;

            string baseUrl = "http://gateway.marvel.com/v1/public/characters";
            Setup(baseUrl);

            try
            {
                await MakeRequest();

                var successfulResponse = JsonConvert.DeserializeObject<CharactersSuccessfulResponse>(_jsonString);
                var data = successfulResponse.Data;
                characters = data.Characters;
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

            Setup(resourceURI);

            try
            {
                await MakeRequest();

                var successfulResponse = JsonConvert.DeserializeObject<ComicSuccessfulResponse>(_jsonString);
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

        private void Setup(string resourceURI)
        {
            _ts = DateUtils.GetCurrentTimestampInMiliseconds();
            _hashString = string.Format("{0}{1}{2}", _ts, _privateKey, _publicKey);
            _md5Hash = _hashString.GetMD5Hash();
            _requestUrl = resourceURI
                + "?apikey=" + _publicKey
                + "&ts=" + _ts
                + "&hash=" + _md5Hash;
        }

        private async Task MakeRequest()
        {
            _response = await _client.GetAsync(_requestUrl);
            _response.EnsureSuccessStatusCode();

            using (var content = _response.Content)
            {
                _jsonString = await content.ReadAsStringAsync();
            }
        }
    }
}
