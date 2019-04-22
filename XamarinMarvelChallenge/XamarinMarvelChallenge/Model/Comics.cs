using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace XamarinMarvelChallenge.Model
{
    public class Comics
    {
        [JsonProperty(PropertyName = "available")]
        public int Available { get; set; }

        [JsonProperty(PropertyName = "collectionURI")]
        public string CollectionURI { get; set; }

        [JsonProperty(PropertyName = "items")]
        public ObservableCollection<ComicsItem> Items { get; set; }

        [JsonIgnore]
        public ObservableCollection<ComicsItem> AssociatedComics
        {
            get
            {
                var associatedComics = new ObservableCollection<ComicsItem>();

                for (int i = 0; i < Items.Count; i++)
                {
                    associatedComics.Add(Items[i]);

                    if (i == 3) // if it is 4
                        break;
                }

                return associatedComics;
            }
        }

        [JsonProperty(PropertyName = "returned")]
        public int Returned { get; set; }
    }
}