using App1.Model;
using App1.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.UWP.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MyItemsDetail : Page {
        public MyItemsDetailViewModel ViewModel { get; set; }
        int quantityCount;

        public MyItemsDetail() {
            this.InitializeComponent();


            DataContext = new MyItemsDetailViewModel();
            ViewModel = new MyItemsDetailViewModel();


            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;


        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case nameof(ViewModel.IsBusy): {
                        if (ViewModel.IsBusy)
                            btnAddItem.IsEnabled = false;
                        else
                            btnAddItem.IsEnabled = true;
                    }
                    break;
            }
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e) {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack) {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            //ViewModel.Item = (MyItem)e.Parameter;
            ViewModel = new MyItemsDetailViewModel((MyItem)e.Parameter);

            txtText.Text = ViewModel.Item.Text;
            txtDesc.Text = ViewModel.Item.Description;
            quantityCount = ViewModel.Quantity;
            txtQuantity.Text = quantityCount.ToString();

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

            ViewModel.Text = txtText.Text;
            ViewModel.Item.Description = txtDesc.Text;
            ViewModel.SaveCommand.Execute(null);
        }
    }
}
