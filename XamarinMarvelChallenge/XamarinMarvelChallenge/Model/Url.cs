using Newtonsoft.Json;

namespace XamarinMarvelChallenge.Model
{
    public class Url
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string UrlValue { get; set; }
    }
}
