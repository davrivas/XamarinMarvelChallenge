using Newtonsoft.Json;
using System.Collections.Generic;

namespace XamarinMarvelChallenge.Model
{
    public class Data
    {
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        [JsonProperty(PropertyName = "results")]
        public List<Character> Characters { get; set; }
    }
}