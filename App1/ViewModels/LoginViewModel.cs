using App1.Helpers;
using App1.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App1.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
        }

        string message = string.Empty;
        public string Message
        {
            get { return message; }
            set { message = value;  OnPropertyChanged(); }
        }
        
        public async Task SignIn()
        {
            try
            {
                IsBusy = true;
				Message = "Signing In...";

                await TryLoginAsync(StoreManager);
            }
            finally
            {

                Message = string.Empty;
                IsBusy = false;
			}
        }

        public static Task<bool> TryLoginAsync(IStoreManager storeManager)
        {
            return Task.FromResult(true);
        }
    }
}
