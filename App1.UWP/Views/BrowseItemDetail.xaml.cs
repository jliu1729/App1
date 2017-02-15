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
using App1.Model;
using App1.ViewModel;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.UWP.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrowseItemDetail : Page {

        public ItemDetailViewModel ViewModel { get; set; }
        int quantityCount = 0;
        public BrowseItemDetail() {
            this.InitializeComponent();


            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            ViewModel = new ItemDetailViewModel();
            DataContext = new ItemDetailViewModel();

        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e) {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack) {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            ViewModel.Item = (Item)e.Parameter;

            txtText.Text = ViewModel.Item.Text;
            txtDesc.Text = ViewModel.Item.Description;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            quantityCount++;
            ViewModel.Quantity = quantityCount;
            txtQuantity.Text = ViewModel.Quantity.ToString();
        }

        private void btnSub_Click(object sender, RoutedEventArgs e) {
            if (quantityCount > 0) {
                quantityCount--;
                ViewModel.Quantity = quantityCount;
                txtQuantity.Text = ViewModel.Quantity.ToString();
            }
        }



        private void btnAddItem_Click(object sender, RoutedEventArgs e) {
            ViewModel.SaveCommand.Execute(null);
        }
    }
}
