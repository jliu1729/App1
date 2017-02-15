using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using App1.ViewModel;

using App1.Helpers;

namespace App1.UWP {
    public class LoginCommand : ICommand {

        public LoginViewModel ViewModel;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            ViewModel = new LoginViewModel();
            return true;
        }

        public async void Execute(object parameter) {
            if (Settings.IsLoggedIn) {
                System.Diagnostics.Debug.WriteLine("Yay");
            } else {
                await ViewModel.SignIn();
            }
        }
    }

}
