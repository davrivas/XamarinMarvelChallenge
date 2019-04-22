using System;
using System.Collections.ObjectModel;

namespace XamarinMarvelChallenge.Model.Comics
{
    public class Comic
    {
        public int id { get; set; }
        public int digitalId { get; set; }
        public string title { get; set; }
        public int issueNumber { get; set; }
        public string variantDescription { get; set; }
        public string description { get; set; }
        public DateTime modified { get; set; }
        public string isbn { get; set; }
        public string upc { get; set; }
        public string diamondCode { get; set; }
        public string ean { get; set; }
        public string issn { get; set; }
        public string format { get; set; }
        public int pageCount { get; set; }
        public ObservableCollection<TextObject> textObjects { get; set; }
        public string resourceURI { get; set; }
        public ObservableCollection<Url> urls { get; set; }
        public Series series { get; set; }
        public ObservableCollection<Variant> variants { get; set; }
        public ObservableCollection<object> collections { get; set; }
        public ObservableCollection<object> collectedIssues { get; set; }
        public ObservableCollection<Date> dates { get; set; }
        public ObservableCollection<Price> prices { get; set; }
        public Thumbnail thumbnail { get; set; }
        public ObservableCollection<Image> images { get; set; }
        public Creators creators { get; set; }
        public Characters characters { get; set; }
        public Stories stories { get; set; }
        public Events events { get; set; }
    }

    #region Temporal
    public class Series
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class Variant
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class Date
    {
        public string type { get; set; }
        public DateTime date { get; set; }
    }

    public class Price
    {
        public string type { get; set; }
        public double price { get; set; }
    }

    public class Thumbnail
    {
        public string path { get; set; }
        public string extension { get; set; }
    }

    public class Image
    {
        public string path { get; set; }
        public string extension { get; set; }
    }

    public class Item
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
        public string role { get; set; }
    }

    public class Creators
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public ObservableCollection<Item> items { get; set; }
        public int returned { get; set; }
    }

    public class Item2
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class Characters
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public ObservableCollection<Item2> items { get; set; }
        public int returned { get; set; }
    }

    public class Item3
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Stories
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public ObservableCollection<Item3> items { get; set; }
        public int returned { get; set; }
    }

    public class Item4
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class Events
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public ObservableCollection<Item4> items { get; set; }
        public int returned { get; set; }
    }
    #endregion
}