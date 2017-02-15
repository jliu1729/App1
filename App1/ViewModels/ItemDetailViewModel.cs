using App1.Helpers;
using App1.Model;
using System;
using System.Threading.Tasks;

namespace App1.ViewModel
{
	public class ItemDetailViewModel : ViewModelBase
    {
        public Action<Item> OnFinished { get; set; }
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
			SaveCommand = new Command(async () => await ExecuteSaveCommand());

			if (item != null)
				Item = item;
			else
				Item = new Item();
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        public Command SaveCommand { get; }

        async Task ExecuteSaveCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var newItem = new MyItem
            {
                Text = Item.Text,
                Description = Item.Description,
                Quantity = Quantity
            };

            try
            {
                if (!Settings.IsLoggedIn)
                {
                    if (!await LoginViewModel.TryLoginAsync(StoreManager))
                        return;
                }

                await StoreManager.MyItemStore.InsertAsync(newItem);

                MyItemsViewModel.IsDirty = true;

                IsBusy = false;
                OnFinished?.Invoke(Item);
                MessageDialog.SendToast("Item has been saved to My Items list.");

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}