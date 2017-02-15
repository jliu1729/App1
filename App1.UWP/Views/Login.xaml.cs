using App1.ViewModel;
using App1.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.UWP.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page {
        // Button signInButton, notNowButton;
        // LinearLayout signingInPanel;
        public Login() {
            this.InitializeComponent();
            App1.App.Initialize();
            DataContext = new LoginViewModel();

        }

        private void ImageBrush_ImageFailed(object sender, ExceptionRoutedEventArgs e) {

        }

        private void btnNotNow_Click(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(MainPivot), null);
        }
    }
}
