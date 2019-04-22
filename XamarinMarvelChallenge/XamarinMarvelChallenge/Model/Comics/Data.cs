using System.Collections.ObjectModel;

namespace XamarinMarvelChallenge.Model.Comics
{
    public class Data
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public ObservableCollection<Comic> results { get; set; }
    }
}