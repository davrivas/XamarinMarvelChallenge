using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using XamarinMarvelChallenge.Extensions;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.MarvelApi
{
    public class RestApi
    {
        private readonly HttpClient _client;

        private const string _publicKey = "f1def8f826359cbe621637efac4cf74c";
        private const string _privateKey = "7fc3dd9c612f602117833595018a48d4b0183d32";

        public RestApi()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
        }

        public async Task<ObservableCollection<Character>> GetCharacters()
        {
            ObservableCollection<Character> characters;

            string baseUrl = "http://gateway.marvel.com/v1/public/characters";
            string requestURL = SetupRequestUrl(baseUrl);

            HttpResponseMessage response;

            try
            {
                response = await _client.GetAsync(requestURL);
                response.EnsureSuccessStatusCode();

                string jsonString;

                using (var content = response.Content)
                {
                    jsonString = await content.ReadAsStringAsync();
                }

                var successfulResponse = JObject.Parse(jsonString);
                var data = (JObject)successfulResponse["data"];
                var results = (JArray)data["results"];

                characters = new ObservableCollection<Character>();

                foreach (var result in results)
                    //for (int i = 0; i < 1; i++)
                {
                    //var result = results[i];
                    string name = (string)result["name"];
                    string description = (string)result["description"];
                    DateTime modified = (DateTime)result["modified"];

                    var thumnail = (JObject)result["thumbnail"];
                    string path = (string)thumnail["path"];
                    string extension = (string)thumnail["extension"];
                    string fullPath = path + "." + extension;

                    var comics = (JObject)result["comics"];
                    string collectionURI = (string)comics["collectionURI"];

                    var comicsCollection = await GetComicsByCharacter(collectionURI);

                    var character = new Character
                    {
                        Name = name,
                        Description = description,
                        Modified = modified,
                        Thumbnail = fullPath,
                        Comics = comicsCollection
                    };
                    characters.Add(character);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                characters = null;
            }

            return characters;
        }

        public async Task<ObservableCollection<Comic>> GetComicsByCharacter(string resourceURI)
        {
            ObservableCollection<Comic> comics;

            string requestURL = SetupRequestUrl(resourceURI);

            HttpResponseMessage response;

            try
            {
                response = await _client.GetAsync(requestURL);
                response.EnsureSuccessStatusCode();

                string jsonString;

                using (var content = response.Content)
                {
                    jsonString = await content.ReadAsStringAsync();
                }

                var successfulResponse = JObject.Parse(jsonString);
                var data = (JObject)successfulResponse["data"];
                var results = (JArray)data["results"];

                comics = new ObservableCollection<Comic>();

                for (int i = 0; i < results.Count; i++)
                {
                    var result = results[i];
                    string title = (string)result["title"];
                    string description = (string)result["description"];

                    var thumbnail = (JObject)result["thumbnail"];
                    string path = (string)thumbnail["path"];
                    string extension = (string)thumbnail["extension"];
                    string fullPath = path + "." + extension;

                    var comic = new Comic
                    {
                        Title = title,
                        Description = description,
                        Thumbnail = fullPath
                    };
                    comics.Add(comic);

                    if (i == 3) // if it is 4
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                comics = null;
            }

            return comics;
        }

        private string SetupRequestUrl(string resourceURI)
        {
            string ts = DateTime.Now.GetCurrentTimestampInMiliseconds();
            string hashString = string.Format("{0}{1}{2}", ts, _privateKey, _publicKey);
            string md5Hash = hashString.GetMD5Hash();
            string requestUrl = string.Format("{0}?apikey={1}&ts={2}&hash={3}", resourceURI, _publicKey, ts, md5Hash);

            return requestUrl;
        }
    }
}
