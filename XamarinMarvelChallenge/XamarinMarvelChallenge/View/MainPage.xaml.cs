using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (GlobalVariables.Characters == null)
                GlobalVariables.Characters = await GlobalVariables.RestApi.GetCharacters();

            _viewModel.GetSearchResults();
            BindingContext = _viewModel;
        }

        private void SortByPickOption(object sender, EventArgs e)
        {
            _viewModel.SortByCommand.Execute(sortByPicker.SelectedItem);
        }

        private void SelectComic(object sender, SelectedItemChangedEventArgs e)
        {
            _viewModel.SelectComicCommand.Execute(e.SelectedItem);
        }
    }
}