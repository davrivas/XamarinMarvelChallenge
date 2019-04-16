using Newtonsoft.Json;

namespace XamarinMarvelChallenge.Model
{
    public class Thumbnail
    {
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; }

        [JsonIgnore]
        public string PathAndExtension => Path + "." + Extension;
    }
}