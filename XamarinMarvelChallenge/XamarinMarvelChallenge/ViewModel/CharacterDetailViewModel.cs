using MvvmHelpers;
using System.Threading.Tasks;
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
            SelectComicCommand = new Command<object>(async (vm) => await SelectComicAsync(vm));
        }

        private async Task SelectComicAsync(object obj)
        {
            var selectedCharacterComic = obj as CharacterComic;
            string resourceURI = selectedCharacterComic.ResourceURI;
            var selectedComic = await App.RestApiObject.GetComicByCharacterAsync(resourceURI);
            var viewModel = new ComicDetailViewModel(selectedCharacterComic, selectedComic);
            ComicPage = new ComicDetail(viewModel);

            MessagingCenter.Send(this, SelectComicMessageName);
        }
    }
}
