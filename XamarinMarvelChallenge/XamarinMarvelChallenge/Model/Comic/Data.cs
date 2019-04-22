using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace XamarinMarvelChallenge.Model.Comic
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
        public ObservableCollection<Comic> Comics { get; set; }
    }
}