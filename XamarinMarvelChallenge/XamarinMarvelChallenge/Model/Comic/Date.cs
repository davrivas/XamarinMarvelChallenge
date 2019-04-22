using Newtonsoft.Json;
using System;

namespace XamarinMarvelChallenge.Model.Comic
{
    public class Date
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime DateValue { get; set; }
    }
}
