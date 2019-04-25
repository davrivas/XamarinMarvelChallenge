using System.Collections.Generic;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.Globals
{
    public static class GlobalVariables
    {
        public static RestApi RestApi { get; set; }
        public static ICollection<Character> Characters { get; set; }
        public static ICollection<CharacterComic> FavoriteComics { get; set; }
    }
}
