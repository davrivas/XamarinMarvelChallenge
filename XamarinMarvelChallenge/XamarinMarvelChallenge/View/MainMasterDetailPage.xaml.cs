using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterDetailPage : MasterDetailPage
    {
        private readonly MainMasterDetailPageViewModel _viewModel;

        public MainMasterDetailPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Detail = new NavigationPage(((Page)Activator.CreateInstance(typeof(MainPage))));

            _viewModel = new MainMasterDetailPageViewModel();
            BindingContext = _viewModel;
        }

        private void SelectMenuItem(object sender, ItemTappedEventArgs e)
        {
            _viewModel.SelectMenuItemCommand.Execute(e.Item);
            (sender as ListView).SelectedItem = null;
            IsPresented = false;
        }
    }
}