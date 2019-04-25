using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMarvelChallenge.Model.App;
using XamarinMarvelChallenge.View;

namespace XamarinMarvelChallenge.ViewModel
{
    public class MainMenuViewModel
    {
        public string Attribution => $"Data provided by Marvel. © 2014-{DateTime.Now.Year} Marvel";
        public string SelectMenuItemMessageName => "SelectMenuItem";

        public Type SelectedDestination { get; private set; }

        public ICollection<AppMenuItem> MenuItems { get; private set; }

        public ICommand SelectMenuItemCommand { get; private set; }

        public MainMenuViewModel()
        {
            MenuItems = new ObservableCollection<AppMenuItem>
            {
                new AppMenuItem("Characters", "characters.png", typeof(CharacterList)),
                new AppMenuItem("Favorite comics", "btn_favourites_primary.png", typeof(FavoriteComics))
            };
            SelectMenuItemCommand = new Command<object>(SelectMenuItem);
        }

        private void SelectMenuItem(object obj)
        {
            var selectedMenuItem = obj as AppMenuItem;
            SelectedDestination = selectedMenuItem.Destination;
            MessagingCenter.Send(this, SelectMenuItemMessageName);
        }
    }
}
