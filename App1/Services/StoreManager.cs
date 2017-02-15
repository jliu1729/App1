using App1.Interfaces;
using App1.Helpers;
using App1.Services.Standard;
using System.Threading.Tasks;

namespace App1.Services.Standard
{
    public class StoreManager : IStoreManager
    {

        public bool UseAuth => true;

		/// <summary>
		/// Syncs all tables.
		/// </summary>
		/// <returns>The all async.</returns>
		/// <param name="syncUserSpecific">If set to <c>true</c> sync user specific.</param>
		public Task<bool> SyncAllAsync(bool syncUserSpecific = true)
		{
            return Task.FromResult(true);
		}

       
        public bool IsInitialized { get; private set; }

#region IStoreManager implementation

        object locker = new object();
        public Task InitializeAsync()
        {
            return Task.FromResult(true);
        }

    IItemStore itemStore;
    public IItemStore ItemStore => itemStore ?? (itemStore = ServiceLocator.Instance.Get<IItemStore>());

    IMyItemStore myItemStore;
    public IMyItemStore MyItemStore => myItemStore ?? (myItemStore = ServiceLocator.Instance.Get<IMyItemStore>());

#endregion

    }
}
