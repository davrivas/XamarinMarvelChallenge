using System;
using System.Collections.Generic;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.Globals
{
    public static class GlobalVariables
    {
        public static RestApi RestApi { get; set; }
        public static ICollection<Character> Characters { get; set; }
        public static ICollection<Comic> FavoriteComics { get; set; }

        public static string Attribution => $"Data provided by Marvel. © 2014-{DateTime.Now.Year} Marvel";
    }
}
