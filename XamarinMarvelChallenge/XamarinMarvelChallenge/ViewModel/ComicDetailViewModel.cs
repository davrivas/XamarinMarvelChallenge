using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.ViewModel
{
    public class ComicDetailViewModel : BaseViewModel
    {
        public CharacterComic SelectedCharacterComic { get; private set; }
        public Comic SelectedComic { get; private set; }

        public ICommand FavoriteCommand { get; private set; }

        public bool IsFavorite => GlobalVariables.FavoriteComics.Contains(SelectedCharacterComic);
        public string FavoriteIcon => IsFavorite ? "btn_favourites_primary.png" : "btn_favourites_default.png";
        public string FavoriteTitle => IsFavorite ? "ADDED TO FAVORITES" : "ADD TO FAVORITES";
        public Color FavoriteTitleColor => IsFavorite ? Color.FromRgb(237, 29, 36) : Color.FromRgb(61, 51, 51);
        public Color FavoriteBackgroundColor => IsFavorite ? Color.FromRgb(50, 40, 39) : Color.FromRgb(240, 240, 240);

        public ComicDetailViewModel(CharacterComic selectedCharacterComic, Comic selectedComic)
        {
            SelectedCharacterComic = selectedCharacterComic;
            SelectedComic = selectedComic;
            Title = SelectedComic.Title;
            FavoriteCommand = new Command(FavoriteMethod);
        }

        private void FavoriteMethod()
        {
            if (IsFavorite)
                GlobalVariables.FavoriteComics.Remove(SelectedCharacterComic);
            else
                GlobalVariables.FavoriteComics.Add(SelectedCharacterComic);

            UpdateProperties();
        }

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(FavoriteIcon));
            OnPropertyChanged(nameof(FavoriteTitle));
            OnPropertyChanged(nameof(FavoriteTitleColor));
            OnPropertyChanged(nameof(FavoriteBackgroundColor));
        }
    }
}
