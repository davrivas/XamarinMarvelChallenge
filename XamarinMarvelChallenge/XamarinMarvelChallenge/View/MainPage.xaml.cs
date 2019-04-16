using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.MarvelApi;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;
        private readonly RestApi _restAPI;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            _restAPI = new RestApi();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (GlobalVariables.Characters == null)
            {
                GlobalVariables.Characters = await _restAPI.GetCharacters();
            }

            _viewModel.Characters = GlobalVariables.Characters;
            BindingContext = _viewModel;
        }
    }
}