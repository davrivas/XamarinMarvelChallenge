using System;
using System.Collections.ObjectModel;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.Model.Characters;

namespace XamarinMarvelChallenge.Globals
{
    public static class GlobalVariables
    {
        public static RestApi RestApi { get; set; }
        public static ObservableCollection<Character> Characters { get; set; }

        public static string Attribution => $"Data provided by Marvel. © 2014-{DateTime.Now.Year} Marvel";
    }
}
