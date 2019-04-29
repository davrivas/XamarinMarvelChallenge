using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.Model;
using XamarinMarvelChallenge.View;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinMarvelChallenge
{
    public partial class App : Application
    {
        #region Global variables
        /// <summary>
        /// This is the default character limit
        /// </summary>
        public const int CharacterLimit = 7;
        /// <summary>
        /// This will be set once
        /// </summary>
        public static int? MaxCharacters { get; set; }
        /// <summary>
        /// This object is used to call the api (I need to see how can I handle this)
        /// </summary>
        public static RestApi RestApiObject { get; set; }
        /// <summary>
        /// This collection is used to store favorite comics
        /// </summary>
        public static ICollection<CharacterComic> FavoriteComics { get; set; }
        #endregion

        public App()
        {
            InitializeComponent();
            MaxCharacters = null;
            RestApiObject = new RestApi();
            FavoriteComics = new ObservableCollection<CharacterComic>();
            MainPage = new NavigationPage(new MainMenu());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
