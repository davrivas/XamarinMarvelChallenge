using MvvmHelpers;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.ViewModel
{
    public class FavoriteComicsViewModel : BaseViewModel
    {
        public ICollection<Comic> FavoriteComics => GlobalVariables.FavoriteComics;

        public ICommand RemoveFromFavoritesCommand { get; private set; }

        public bool HasComics => FavoriteComics.Count > 0;
        public bool DoesNotHaveComics => FavoriteComics.Count == 0;

        public FavoriteComicsViewModel()
        {
            Title = "Favorite comics (" + GlobalVariables.FavoriteComics.Count + ")";
            RemoveFromFavoritesCommand = new Command<object>(RemoveFromFavorites);
        }

        private void RemoveFromFavorites(object obj)
        {
            var selectedComic = obj as Comic;
            GlobalVariables.FavoriteComics.Remove(selectedComic);
            UpdateProperties();
        }

        public void UpdateProperties()
        {
            Title = "Favorite comics (" + GlobalVariables.FavoriteComics.Count + ")";
            OnPropertyChanged(nameof(FavoriteComics));
            OnPropertyChanged(nameof(HasComics));
            OnPropertyChanged(nameof(DoesNotHaveComics));
        }
    }
}
