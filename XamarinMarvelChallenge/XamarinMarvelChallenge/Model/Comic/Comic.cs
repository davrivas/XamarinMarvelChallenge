using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace XamarinMarvelChallenge.Model.Comic
{
    public class Comic
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "digitalId")]
        public int DigitalId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "issueNumber")]
        public int IssueNumber { get; set; }

        [JsonProperty(PropertyName = "variantDescription")]
        public string VariantDescription { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "modified")]
        public DateTime Modified { get; set; }

        [JsonProperty(PropertyName = "isbn")]
        public string Isbn { get; set; }

        [JsonProperty(PropertyName = "upc")]
        public string Upc { get; set; }

        [JsonProperty(PropertyName = "diamondCode")]
        public string DiamondCode { get; set; }

        [JsonProperty(PropertyName = "Ean")]
        public string Ean { get; set; }

        [JsonProperty(PropertyName = "issn")]
        public string Issn { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "pageCount")]
        public int PageCount { get; set; }

        [JsonProperty(PropertyName = "textObjects")]
        public ObservableCollection<TextObject> TextObjects { get; set; }

        [JsonProperty(PropertyName = "resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty(PropertyName = "urls")]
        public ObservableCollection<Url> Urls { get; set; }

        [JsonProperty(PropertyName = "series")]
        public Series Series { get; set; }

        [JsonProperty(PropertyName = "variants")]
        public ObservableCollection<Variant> Variants { get; set; }

        [JsonProperty(PropertyName = "collections")]
        public ObservableCollection<object> Collections { get; set; }

        [JsonProperty(PropertyName = "collectedIssues")]
        public ObservableCollection<object> CollectedIssues { get; set; }

        [JsonProperty(PropertyName = "dates")]
        public ObservableCollection<Date> Dates { get; set; }

        [JsonProperty(PropertyName = "prices")]
        public ObservableCollection<Price> Prices { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty(PropertyName = "images")]
        public ObservableCollection<Image> Images { get; set; }

        [JsonProperty(PropertyName = "creators")]
        public Creators Creators { get; set; }

        [JsonProperty(PropertyName = "characters")]
        public Characters Characters { get; set; }

        [JsonProperty(PropertyName = "Stories")]
        public Stories Stories { get; set; }

        [JsonProperty(PropertyName = "events")]
        public Events Events { get; set; }
    }
}