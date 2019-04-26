﻿using System;
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
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<CharacterListViewModel>(_viewModel, 
                _viewModel.SelectCharacterMessageName, 
                async (viewModel) => await HandleSelectCharacterAsync(viewModel));
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
            charactersListView.SelectedItem = null;
        }

        private async Task HandleSelectCharacterAsync(CharacterListViewModel viewModel)
        {
            await Navigation.PushAsync(viewModel.CharacterDetailPage);
        }
    }
}