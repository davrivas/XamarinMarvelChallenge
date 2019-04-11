using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinMarvelChallenge.MarvelApi
{
    class MarvelApi
    {
        public dynamic Characters { get; } // GetCharacters
        private readonly HttpClient _client = new HttpClient();
        private readonly string _apiBaseEndpoint = ConfigurationManager.AppSettings["ApiBaseEndpoint"];
        private readonly string _publicKey = ConfigurationManager.AppSettings["PublicKey"];
        private readonly string _privateKey = ConfigurationManager.AppSettings["PrivateKey"];
        public readonly string Attribution = $"Data provided by Marvel. © {DateTime.Now.Year} Marvel";

        // example call
        // ts = timestamp
        // Hash = md5(ts+PrivateKey+PublicKey)
        // http://gateway.marvel.com/v1/public/comics?ts=15549922310007&apikey=f1def8f826359cbe621637efac4cf74c&hash=7bbded379bc8e5b8443a934076b6e84e

        public MarvelApi()
        {

        }

        public /*async Task<dynamic>*/ void GetCharacters()
        {
            var timestamp = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            var hashString = string.Format("{0}{1}{2}", timestamp, _privateKey, _publicKey);
            var hash = CreateHash(hashString);
            var requestURL = string.Format("{0}/characters?ts={0}&apiKey={1}&hash={2}", _apiBaseEndpoint, timestamp, hash);
        }

        private string CreateHash(string hashString)
        {
            throw new NotImplementedException();
        }
    }
}
