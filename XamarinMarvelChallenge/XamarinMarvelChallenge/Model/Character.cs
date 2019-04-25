using System;
using System.Collections.ObjectModel;

namespace XamarinMarvelChallenge.Model
{
    public class Character
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDescription => !string.IsNullOrWhiteSpace(Description);
        public bool DoesNotHaveDescription => string.IsNullOrWhiteSpace(Description);
        public DateTime Modified { get; set; }
        public string Thumbnail { get; set; }
        public ObservableCollection<Comic> Comics { get; set; }
        public bool HasComics => Comics.Count > 0;
        public bool DoesNotHaveComics => Comics.Count == 0;
    }
}
