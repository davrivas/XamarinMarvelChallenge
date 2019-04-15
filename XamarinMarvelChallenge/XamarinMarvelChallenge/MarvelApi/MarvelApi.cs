using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.MarvelApi
{
    public class MarvelApi
    {
        private readonly HttpClient _client;
        private const string _apiBaseEndpoint = "http://gateway.marvel.com/v1/public";
        private const string _publicKey = "f1def8f826359cbe621637efac4cf74c";
        private const string _privateKey = "7fc3dd9c612f602117833595018a48d4b0183d32";

        public static string Attribution => "Data provided by Marvel. © 2014 Marvel";

        public MarvelApi()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
        }

        public async Task<List<Character>> GetCharacters()
        {
            List<Character> characters;

            string ts = ((long)(DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString();
            string hashString = string.Format("{0}{1}{2}", ts, _privateKey, _publicKey);
            string hash = CreateHash(hashString);
            string requestURL = $"{_apiBaseEndpoint}/characters?apikey={_publicKey}&ts={ts}&hash={hash}";
            var url = new Uri(requestURL);

            try
            {                
                var response = await _client.GetAsync(url);

                string json;

                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                if (response.IsSuccessStatusCode)
                {
                    var successfulResponse = JsonConvert.DeserializeObject<SuccessfulResponse>(json);
                    var data = successfulResponse.data;
                    characters = data.Characters;
                }
                else
                {
                    Debug.WriteLine(json);
                    characters = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                characters = null;
            }

            return characters;

        }

        private string CreateHash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
    }
}
