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
using System.Collections.Specialized;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.UWP.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPivot : Page {
        public BrowseItemsViewModel browseViewModel { get; set; }
        public MyItemsViewModel myViewModel { get; set; }
        //private List<Item> Items;

        public MainPivot() {

            this.InitializeComponent();

            myViewModel = new MyItemsViewModel();
            browseViewModel = new BrowseItemsViewModel();

            gvItems.ItemClick += GvItems_ItemClick;
            myItems.ItemClick += MyItems_ItemClick;
            myViewModel.Items.CollectionChanged += Items_CollectionChanged1;

            if (myViewModel.Items.Count == 0)
                myViewModel.LoadItemsCommand.Execute(null);

            browseViewModel.Items.CollectionChanged += Items_CollectionChanged;


            if (browseViewModel.Items.Count == 0)
                browseViewModel.LoadItemsCommand.Execute(null);
        }

        private void MyItems_ItemClick(object sender, ItemClickEventArgs e) {
            var item = e.ClickedItem as Item;
            if (item == null) {
                return;
            }

            this.Frame.Navigate(typeof(MyItemsDetail), item);

            myItems.SelectedItem = null;
        }

        private void Items_CollectionChanged1(object sender, NotifyCollectionChangedEventArgs e) {
            myItems.ItemsSource = myViewModel.Items;
        }

        private void GvItems_ItemClick(object sender, ItemClickEventArgs e) {
            var item = e.ClickedItem as Item;
            if (item == null) {
                return;
            }
            this.Frame.Navigate(typeof(BrowseItemDetail), item);

            gvItems.SelectedItem = null;

        }


        void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            gvItems.ItemsSource = browseViewModel.Items;


        }

    }
}
