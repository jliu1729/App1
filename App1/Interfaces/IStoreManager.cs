using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Interfaces
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();
        IItemStore ItemStore { get; }
        IMyItemStore MyItemStore { get; }
        Task<bool> SyncAllAsync(bool syncUserSpecific = true);
        bool UseAuth { get; }
    }
}
