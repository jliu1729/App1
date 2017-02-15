using App1.Helpers;
using App1.Model;
using System;
using System.Diagnostics;
using System.Linq;

namespace App1.ViewModel
{
	public class BrowseItemsViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Item> Items { get;}
        public Action<ItemDetailViewModel> OnNavigateToDetails { get; set; }
        public BrowseItemsViewModel()
        {
			Title = "Browse";
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            GoToDetailsCommand = new Command<string>(ExecuteGoToDetailsCommand);
        }

        public Command LoadItemsCommand { get;}

        async void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await StoreManager.ItemStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                //Handle exception here
                Debug.WriteLine(ex);

                MessageDialog.SendMessage("Unable to load items.", "Error");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command<string> GoToDetailsCommand { get; }
        ItemDetailViewModel detailsViewModel;
        void ExecuteGoToDetailsCommand(string id)
        {
            if (IsBusy)
                return;

            var selectedItem = Items.FirstOrDefault(i => i.Id == id);

            detailsViewModel = new ItemDetailViewModel(selectedItem);
            detailsViewModel.OnFinished += OnFinished;

            OnNavigateToDetails(detailsViewModel);
        }

        void OnFinished(Item item)
        {
            detailsViewModel.OnFinished -= OnFinished;
        }
    }
}
