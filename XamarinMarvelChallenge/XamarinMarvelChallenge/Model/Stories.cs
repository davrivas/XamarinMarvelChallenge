using Newtonsoft.Json;
using System.Collections.Generic;

namespace XamarinMarvelChallenge.Model
{
    public class Stories
    {
        [JsonProperty(PropertyName = "available")]
        public int Available { get; set; }
        [JsonProperty(PropertyName = "collectionURI")]
        public string CollectionURI { get; set; }
        [JsonProperty(PropertyName = "items")]
        public List<object> Items { get; set; }
        [JsonProperty(PropertyName = "returned")]
        public int Returned { get; set; }
    }
}