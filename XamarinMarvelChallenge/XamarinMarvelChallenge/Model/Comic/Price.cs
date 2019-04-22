using Newtonsoft.Json;

namespace XamarinMarvelChallenge.Model.Comic
{
    public class Price
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double PriceValue { get; set; }
    }
}
