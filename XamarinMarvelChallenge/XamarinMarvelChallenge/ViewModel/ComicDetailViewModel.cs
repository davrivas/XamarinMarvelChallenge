using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.ViewModel
{
    public class ComicDetailViewModel : BaseViewModel
    {
        private Comic _selectedComic;

        public Comic SelectedComic
        {
            get { return _selectedComic; }
            set
            {
                SetProperty(ref _selectedComic, value);
                UpdateProperties();
            }
        }

        public ICommand FavoriteCommand { get; private set; }

        public string FavoriteTitle => SelectedComic.IsFavorite ? "ADDED TO FAVORITES" : "ADD TO FAVORITES";
        public Color FavoriteTitleColor => SelectedComic.IsFavorite ? Color.FromRgb(237, 29, 36) : Color.FromRgb(61, 51, 51);
        public Color FavoriteBackgroundColor => SelectedComic.IsFavorite ? Color.FromRgb(50, 40, 39) : Color.FromRgb(240, 240, 240);

        public ComicDetailViewModel(Comic selectedComic)
        {
            SelectedComic = selectedComic;
            FavoriteCommand = new Command(FavoriteMethod);
        }

        private void FavoriteMethod()
        {
            if (SelectedComic.IsFavorite)
                GlobalVariables.FavoriteComics.Remove(SelectedComic);
            else
                GlobalVariables.FavoriteComics.Add(SelectedComic);

            UpdateProperties();
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(FavoriteTitle));
            OnPropertyChanged(nameof(FavoriteTitleColor));
            OnPropertyChanged(nameof(FavoriteBackgroundColor));
        }
    }
}
