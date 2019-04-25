using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.Model.App;
using XamarinMarvelChallenge.View;

namespace XamarinMarvelChallenge.ViewModel
{
    public class MainMenuViewModel
    {
        public string Attribution => GlobalVariables.Attribution;

        public ICollection<AppMenuItem> MenuItems { get; private set; }

        public ICommand SelectMenuItemCommand { get; private set; }

        public MainMenuViewModel()
        {
            MenuItems = new ObservableCollection<AppMenuItem>
            {
                new AppMenuItem("Characters", "characters.png", typeof(CharacterList)),
                new AppMenuItem("Favorites", "btn_favourites_primary.png", typeof(Page)) // must be Favorites page
            };
            SelectMenuItemCommand = new Command<object>(SelectMenuItem);
        }

        private void SelectMenuItem(object obj)
        {
            var selectedMenuItem = obj as AppMenuItem;
            var destination = selectedMenuItem.Destination;
            //GlobalVariables.CurrentMasterDetailPage.Detail = new NavigationPage(((Page)Activator.CreateInstance(destination)));
        }
    }
}
