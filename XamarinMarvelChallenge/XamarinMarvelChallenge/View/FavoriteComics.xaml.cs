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
    }
}