using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : MasterDetailPage
    {
        private readonly MainMenuViewModel _viewModel;

        public MainMenu()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Detail = new NavigationPage(((Page)Activator.CreateInstance(typeof(CharacterList))));

            _viewModel = new MainMenuViewModel();
            BindingContext = _viewModel;
        }

        private void SelectMenuItem(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            _viewModel.SelectMenuItemCommand.Execute(e.Item);
            (sender as ListView).SelectedItem = null;
            IsPresented = false;
        }
    }
}