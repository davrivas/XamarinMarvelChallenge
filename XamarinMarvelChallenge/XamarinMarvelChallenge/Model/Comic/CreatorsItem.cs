using Newtonsoft.Json;

namespace XamarinMarvelChallenge.Model.Comic
{
    public class CreatorsItem
    {
        [JsonProperty(PropertyName = "resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
    }
}
