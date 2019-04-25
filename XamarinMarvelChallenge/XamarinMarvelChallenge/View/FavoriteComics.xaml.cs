using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteComics : ContentPage
    {
        private readonly FavoriteComicsViewModel _viewModel;

        public FavoriteComics()
        {
            InitializeComponent();
            _viewModel = new FavoriteComicsViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<FavoriteComicsViewModel>(_viewModel,
                _viewModel.SelectComicMessageName,
                async (viewModel) => await HandleSelectComic(viewModel));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<FavoriteComicsViewModel>(_viewModel, _viewModel.SelectComicMessageName);
        }

        private async Task HandleSelectComic(FavoriteComicsViewModel viewModel)
        {
            await Navigation.PushAsync(viewModel.ComicPage);
        }
    }
}