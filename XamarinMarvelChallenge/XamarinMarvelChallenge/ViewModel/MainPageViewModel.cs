using MvvmHelpers;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinMarvelChallenge.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        public string ProceedMessage => "ProceedToApp";
        public ICommand ProceedCommand { get; private set; }

        public MainPageViewModel()
        {
            IsBusy = true;
            IsNotBusy = false;
            ProceedCommand = new Command(ProceedToApp);
        }

        private void ProceedToApp()
        {
            MessagingCenter.Send(this, ProceedMessage);
        }
    }
}
