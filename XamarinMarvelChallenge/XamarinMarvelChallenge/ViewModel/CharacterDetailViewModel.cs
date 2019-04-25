using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMarvelChallenge.Model;
using XamarinMarvelChallenge.View;

namespace XamarinMarvelChallenge.ViewModel
{
    public class CharacterDetailViewModel : BaseViewModel
    {
        public string SelectComicMessageName => "SelectComic";

        public Character SelectedCharacter { get; private set; }
        public Page ComicPage { get; private set; }

        public ICommand SelectComicCommand { get; private set; }

        public CharacterDetailViewModel(Character selectedCharacter)
        {
            SelectedCharacter = selectedCharacter;
            Title = SelectedCharacter.Name;
            SelectComicCommand = new Command<object>(SelectComic);
        }

        private void SelectComic(object obj)
        {
            var selectedComic = obj as Comic;
            var viewModel = new ComicDetailViewModel(selectedComic);
            ComicPage = new ComicDetail(viewModel);

            MessagingCenter.Send(this, SelectComicMessageName);
        }
    }
}
