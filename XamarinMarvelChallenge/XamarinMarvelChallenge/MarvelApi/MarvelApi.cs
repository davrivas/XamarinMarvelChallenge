﻿using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XamarinMarvelChallenge.MarvelApi
{
    public class MarvelApi
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

        public async Task<dynamic> GetCharacters()
        {
            try
            {
                var timestamp = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
                var hashString = string.Format("{0}{1}{2}", timestamp, _privateKey, _publicKey);
                var hash = CreateHash(hashString);
                var requestURL = string.Format("{0}/characters?ts={1}&apiKey={2}&hash={3}", _apiBaseEndpoint, timestamp, _privateKey, hash);
                var url = new Uri(requestURL);
                var response = await _client.GetAsync(url);

                string json;

                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                dynamic characters = JsonConvert.DeserializeObject<dynamic>(json);

                return characters;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
            
        }

        private string CreateHash(string hashString)
        {
            var hash = string.Empty;

            using (MD5 md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(hashString));
                var stringBuilder = new StringBuilder();

                for (var i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }

                hash = stringBuilder.ToString();
            }

            return hash;
        }
    }
}
