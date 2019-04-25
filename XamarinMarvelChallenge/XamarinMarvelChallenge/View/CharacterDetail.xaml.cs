using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterDetail : ContentPage
    {
        private readonly CharacterDetailViewModel _viewModel;

        public CharacterDetail(CharacterDetailViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<CharacterDetailViewModel>(_viewModel, 
                _viewModel.SelectComicMessageName,
                async (viewModel) => await HandleSelectComic(viewModel));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<CharacterDetailViewModel>(_viewModel, _viewModel.SelectComicMessageName);
        }

        private void ComicsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            _viewModel.SelectComicCommand.Execute(e.Item);
            comicsListView.SelectedItem = null;
        }

        private async Task HandleSelectComic(CharacterDetailViewModel viewModel)
        {
            await Navigation.PushAsync(viewModel.ComicPage);
        }
    }
}