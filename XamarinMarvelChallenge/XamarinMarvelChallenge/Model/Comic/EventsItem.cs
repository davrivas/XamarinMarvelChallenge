using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinMarvelChallenge.Model.Comic
{
    public class EventsItem
    {
        [JsonProperty(PropertyName = "resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
