using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.Model.Characters;
using XamarinMarvelChallenge.Model.Comic;

namespace XamarinMarvelChallenge.Globals
{
    public static class GlobalVariables
    {
        public static Page CurrentPage => Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
        public static ContentPage CurrentContenPage => CurrentPage as ContentPage;
        public static MasterDetailPage CurrentMasterDetailPage => CurrentPage as MasterDetailPage;

        public static RestApi RestApi { get; set; }
        public static ICollection<Character> Characters { get; set; }
        public static ICollection<Comic> FavoriteComics { get; set; }

        public static string Attribution => $"Data provided by Marvel. © 2014-{DateTime.Now.Year} Marvel";
    }
}
