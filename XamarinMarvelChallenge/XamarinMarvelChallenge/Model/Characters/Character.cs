using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace XamarinMarvelChallenge.Model.Characters
{
    public class Character : ObservableObject
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "modified")]
        public DateTime Modified { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty(PropertyName = "resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty(PropertyName = "comics")]
        public Comics Comics { get; set; }

        [JsonProperty(PropertyName = "series")]
        public Series Series { get; set; }

        [JsonProperty(PropertyName = "stories")]
        public Stories Stories { get; set; }

        [JsonProperty(PropertyName = "events")]
        public Events Events { get; set; }

        [JsonProperty(PropertyName = "urls")]
        public ObservableCollection<Url> Urls { get; set; }
    }
}
