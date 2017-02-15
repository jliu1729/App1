using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Helpers;
using Plugin.Share;

namespace App1.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
        {
            Title = "About";

			OpenWebCommand = new Command(() => CrossShare.Current.OpenBrowser("https://xamarin.com/platform"));
        }


        /// <summary>
        /// Command to open browser to xamarin.com
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}
