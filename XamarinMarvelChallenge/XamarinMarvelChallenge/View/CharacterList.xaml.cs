using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.Globals;
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
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (GlobalVariables.Characters == null)
            {
                if (_viewModel.IsBusy && !_viewModel.IsNotBusy)
                {
                    GlobalVariables.Characters = await GlobalVariables.RestApi.GetCharacters();
                    _viewModel.IsBusy = false;
                    _viewModel.IsNotBusy = true;

                }
            }

            _viewModel.GetSearchResults();
            BindingContext = _viewModel;

            MessagingCenter.Subscribe<CharacterListViewModel>(_viewModel, 
                _viewModel.SelectCharacterMessageName, 
                async (viewModel) => await HandleSelectCharacter(viewModel));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<CharacterListViewModel>(_viewModel, _viewModel.SelectCharacterMessageName);
        }

        private void SortByPickOption(object sender, EventArgs e)
        {
            _viewModel.SortByCommand.Execute(sortByPicker.SelectedItem);
        }

        private void SelectCharacter(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            _viewModel.SelectCharacterCommand.Execute(e.SelectedItem);
            (sender as ListView).SelectedItem = null;
        }

        private async Task HandleSelectCharacter(CharacterListViewModel viewModel)
        {
            await Navigation.PushAsync(viewModel.CharacterDetailPage);
        }
    }
}