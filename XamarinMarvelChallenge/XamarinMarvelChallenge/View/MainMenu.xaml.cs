using System;
using System.Threading.Tasks;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MainMenuViewModel>(_viewModel, 
                _viewModel.SelectMenuItemMessageName,
                HandleSelectMenuItem);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void SelectMenuItem(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            _viewModel.SelectMenuItemCommand.Execute(e.Item);
            menuItemsListView.SelectedItem = null;
            IsPresented = false;
        }

        private void HandleSelectMenuItem(MainMenuViewModel viewModel)
        {
            Detail = new NavigationPage(((Page)Activator.CreateInstance(viewModel.SelectedDestination)));
        }
    }
}