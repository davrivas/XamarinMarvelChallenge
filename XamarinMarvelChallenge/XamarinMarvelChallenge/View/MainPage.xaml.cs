using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (GlobalVariables.Characters == null)
            {
                if (_viewModel.IsBusy && !_viewModel.IsNotBusy)
                {
                    GlobalVariables.Characters = await GlobalVariables.RestApi.GetCharacters();
                    _viewModel.IsBusy = false;
                    _viewModel.IsNotBusy = true;
                }
            }

            MessagingCenter.Subscribe<MainPageViewModel>(_viewModel, _viewModel.ProceedMessage, async (sender) =>
            {
                await Navigation.PushAsync(new MainMasterDetailPage());
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<MainPageViewModel>(_viewModel, _viewModel.ProceedMessage);
        }
    }
}