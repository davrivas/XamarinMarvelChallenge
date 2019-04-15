using System.Collections.ObjectModel;
using XamarinMarvelChallenge.Model;

namespace XamarinMarvelChallenge.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<Character> Characters { get; set; }

        public MainPageViewModel()
        {

        }
    }
}
