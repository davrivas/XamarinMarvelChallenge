using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.Model.Comic;
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
            GlobalVariables.FavoriteComics = new ObservableCollection<Comic>();
            MainPage = new NavigationPage(new MainPage());
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
