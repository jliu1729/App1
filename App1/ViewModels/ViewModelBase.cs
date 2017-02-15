using App1.Helpers;
using App1.Interfaces;

namespace App1.ViewModel
{
	public class ViewModelBase : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
		public IStoreManager StoreManager => ServiceLocator.Instance.Get<IStoreManager>();

        public IMessageDialog MessageDialog => ServiceLocator.Instance.Get<IMessageDialog>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}