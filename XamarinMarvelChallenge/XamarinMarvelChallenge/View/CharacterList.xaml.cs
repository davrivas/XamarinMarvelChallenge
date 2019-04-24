using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterList : ContentPage
    {
        private readonly CharacterListViewModel _viewModel;

        public CharacterList()
        {
            InitializeComponent();
            _viewModel = new CharacterListViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<CharacterListViewModel>(_viewModel, _viewModel.SelectComicMessageName, async (sender) =>
            {
                await HandleSelectComic(sender);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<CharacterListViewModel>(_viewModel, _viewModel.SelectComicMessageName);
        }

        private void SortByPickOption(object sender, EventArgs e)
        {
            _viewModel.SortByCommand.Execute(sortByPicker.SelectedItem);
        }

        private void SelectComic(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            _viewModel.SelectComicCommand.Execute(e.SelectedItem);
            (sender as ListView).SelectedItem = null;
        }

        private async Task HandleSelectComic(CharacterListViewModel viewModel)
        {
            await Navigation.PushAsync(viewModel.ComicDetailPage);
        }
    }
}