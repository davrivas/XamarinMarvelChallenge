using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace XamarinMarvelChallenge.Model.Comic
{
    public class Stories
    {
        [JsonProperty(PropertyName = "available")]
        public int Available { get; set; }

        [JsonProperty(PropertyName = "collectionURI")]
        public string CollectionURI { get; set; }

        [JsonProperty(PropertyName = "items")]
        public ObservableCollection<StoriesItem> Items { get; set; }

        [JsonProperty(PropertyName = "returned")]
        public int Returned { get; set; }
    }
}
