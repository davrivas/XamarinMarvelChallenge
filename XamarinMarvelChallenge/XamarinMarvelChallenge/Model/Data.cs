using Newtonsoft.Json;
using System.Collections.Generic;

namespace XamarinMarvelChallenge.Model
{
    public class Data
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        [JsonProperty(PropertyName = "results")]
        public List<Character> Characters { get; set; }
    }
}