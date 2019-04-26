using MvvmHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model;
using XamarinMarvelChallenge.View;

namespace XamarinMarvelChallenge.ViewModel
{
    public class FavoriteComicsViewModel : BaseViewModel
    {
        public string SelectComicMessageName => "SelectComic";

        public ICollection<CharacterComic> FavoriteComics => GlobalVariables.FavoriteComics;

        public ICommand SelectComicCommand { get; private set; }
        public ICommand RemoveFromFavoritesCommand { get; private set; }

        public Page ComicPage { get; private set; }

        public bool HasComics => FavoriteComics.Count > 0;
        public bool DoesNotHaveComics => FavoriteComics.Count == 0;

        public FavoriteComicsViewModel()
        {
            Title = GetTitle();
            SelectComicCommand = new Command<object>(async (vm) => await SelectComic(vm));
            RemoveFromFavoritesCommand = new Command<object>(RemoveFromFavorites);
        }

        private async Task SelectComic(object obj)
        {
            var selectedCharacterComic = obj as CharacterComic;
            string resourceURI = selectedCharacterComic.ResourceURI;
            var selectedComic = await GlobalVariables.RestApi.GetComicByCharacterAsync(resourceURI);
            var viewModel = new ComicDetailViewModel(selectedCharacterComic, selectedComic);
            ComicPage = new ComicDetail(viewModel);

            MessagingCenter.Send(this, SelectComicMessageName);
        }

        private void RemoveFromFavorites(object obj)
        {
            var selectedComic = obj as CharacterComic;
            GlobalVariables.FavoriteComics.Remove(selectedComic);
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            Title = GetTitle();
            OnPropertyChanged(nameof(FavoriteComics));
            OnPropertyChanged(nameof(HasComics));
            OnPropertyChanged(nameof(DoesNotHaveComics));
        }

        private string GetTitle()
        {
            string title = "Favorite comics";

            if (GlobalVariables.FavoriteComics.Count > 0)
                title += " (" + GlobalVariables.FavoriteComics.Count + ")";

            return title;
        }
    }
}
