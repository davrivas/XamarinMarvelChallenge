using Newtonsoft.Json;

namespace XamarinMarvelChallenge.Model
{
    public class ComicsItem
    {
        [JsonProperty(PropertyName = "resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}