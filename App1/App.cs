using System.Collections.Generic;
#if __ANDROID__
using App1.Droid.Helpers;
#elif __IOS__
using App1.iOS.Helpers;
#elif WINDOWS_UWP
using App1.UWP.Helpers;
#endif
using App1.Helpers;
using App1.Interfaces;

using App1.Services.Standard;
using System;

namespace App1
{
    public class App
    {

        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IItemStore, ItemStore>();
            ServiceLocator.Instance.Register<IMyItemStore, MyItemStore>();
            ServiceLocator.Instance.Register<IStoreManager, StoreManager>();
            ServiceLocator.Instance.Register<IMessageDialog, MessageDialog>();
        }
    }
}

