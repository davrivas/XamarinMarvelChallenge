﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMarvelChallenge.Globals;
using XamarinMarvelChallenge.ViewModel;

namespace XamarinMarvelChallenge.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComicDetail : ContentPage
    {
        private readonly ComicDetailViewModel _viewModel;

        public ComicDetail(ComicDetailViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            string resourceURI = _viewModel.SelectedCharacterComic.ResourceURI;
            _viewModel.SelectedComic = await GlobalVariables.RestApi.GetComicByCharacter(resourceURI);
        }
    }
}