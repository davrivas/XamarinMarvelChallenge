using Newtonsoft.Json;

namespace XamarinMarvelChallenge.Model.Comic
{
    public class TextObject
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}