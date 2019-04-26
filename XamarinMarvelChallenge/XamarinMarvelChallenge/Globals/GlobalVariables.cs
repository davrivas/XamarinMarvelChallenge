using System.Collections.Generic;
using System.Collections.ObjectModel;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.Globals
{
    public static class GlobalVariables
    {
        public const int CharacterLimit = 7;
        public static RestApi RestApi { get; set; }
        public static ObservableCollection<Character> Characters { get; set; }
        public static ICollection<CharacterComic> FavoriteComics { get; set; }
    }
}
