using Newtonsoft.Json;

namespace XamarinMarvelChallenge.Model.Comic
{
    public class Image
    {
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; }
    }
}
