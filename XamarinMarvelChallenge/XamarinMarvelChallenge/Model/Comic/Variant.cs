using Newtonsoft.Json;

namespace XamarinMarvelChallenge.Model.Comic
{
    public class Variant
    {
        [JsonProperty(PropertyName = "resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
