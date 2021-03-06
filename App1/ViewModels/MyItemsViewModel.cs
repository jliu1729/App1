﻿using App1.Helpers;
using App1.Model;
using System;
using System.Diagnostics;
using System.Linq;

namespace App1.ViewModel
{
    public class MyItemsViewModel : ViewModelBase
    {
        public ObservableRangeCollection<MyItem> Items { get; private set; }
        public Action<MyItemsDetailViewModel> OnNavigateToDetails { get; set; }
        public static bool IsDirty { get; set; }
		public bool IsDeleting { get; set; }

        public MyItemsViewModel()
        {
			Title = "My Items";
            Items = new ObservableRangeCollection<MyItem>();
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            DeleteItemCommand = new Command<string>(ExecuteDeleteItemCommand);
            EditCommand = new Command<string>(ExecuteEditCommand);
        }

        public Command LoadItemsCommand { get; }

        async void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await StoreManager.MyItemStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
                IsDirty = false;

            }
            catch (Exception ex)
            {
                //Handle exception here
                Debug.WriteLine("Unable to get items: " + ex);
            }
            finally
            {
                IsBusy = false;
            }
        }



        public Command<string> DeleteItemCommand { get; }

        async void ExecuteDeleteItemCommand(string id)
        {
            if (IsBusy)
                return;

            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return;

			IsDeleting = true;
            IsBusy = true;
            try
            {
				Items.Remove(item);
                await StoreManager.MyItemStore.RemoveAsync(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to delte: " + ex);
            }
            finally
            {
                IsBusy = false;
				IsDeleting = false;
            }
        }

        public Command<string> EditCommand { get; }
        MyItemsDetailViewModel detailsViewModel;
        void ExecuteEditCommand(string id)
        {
            if (IsBusy)
                return;

            var selectedItem = Items.FirstOrDefault(i => i.Id == id);

            detailsViewModel = new MyItemsDetailViewModel(selectedItem);
            detailsViewModel.OnFinished += OnFinished;

            OnNavigateToDetails(detailsViewModel);
        }

        void OnFinished(Item item)
        {
            detailsViewModel.OnFinished -= OnFinished;
        }

    }
}
