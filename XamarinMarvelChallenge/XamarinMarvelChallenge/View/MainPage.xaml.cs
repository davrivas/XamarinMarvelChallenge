using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

            _viewModel.Characters = await _restAPI.GetCharacters();

            BindingContext = _viewModel;
        }
    }
}