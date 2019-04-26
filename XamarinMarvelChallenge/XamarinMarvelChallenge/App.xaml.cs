using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.Model;
using XamarinMarvelChallenge.View;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinMarvelChallenge
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            GlobalVariables.RestApi = new RestApi();
            //Task.Run(() => DownloadCharactersAsync()).Wait();
            GlobalVariables.FavoriteComics = new ObservableCollection<CharacterComic>();
            MainPage = new NavigationPage(new MainMenu());
        }

        //private async Task DownloadCharactersAsync()
        //{
        //    GlobalVariables.Characters = await GlobalVariables.RestApi.GetCharactersAsync();
        //}

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
