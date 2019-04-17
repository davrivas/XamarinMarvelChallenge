using System.Collections.ObjectModel;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.Globals
{
    public static class GlobalVariables
    {
        public static RestApi RestApi { get; set; }
        public static ObservableCollection<Character> Characters { get; set; }
    }
}
