using Newtonsoft.Json;

namespace XamarinMarvelChallenge.Model.Characters
{
    public class CharactersSuccessfulResponse
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "copyright")]
        public string Copyright { get; set; }

        [JsonProperty(PropertyName = "attributionText")]
        public string AttributionText { get; set; }

        [JsonProperty(PropertyName = "attributionHTML")]
        public string AttributionHTML { get; set; }

        [JsonProperty(PropertyName = "etag")]
        public string Etag { get; set; }

        [JsonProperty(PropertyName = "data")]
        public Data Data { get; set; }
    }
}
