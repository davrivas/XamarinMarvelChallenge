using System.Collections.ObjectModel;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.Globals
{
    public static class GlobalVariables
    {
        public static ObservableCollection<Character> Characters { get; set; }
        public static ObservableCollection<Character> FavoriteCharacters { get; set; }
    }
}
