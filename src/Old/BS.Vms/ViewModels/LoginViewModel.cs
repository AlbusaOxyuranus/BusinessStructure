using BlackBee.Toolkit.Base;
using System.Windows.Input;
using BlackBee.Toolkit.Commands;
using System.Threading.Tasks;
using BS.WPF.Views.Pages;
using Geliada.DesktopApp.ViewModels.Menu;

namespace BS.Vms.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private string _userName;
        private string _userPassword;
        public IAsyncCommand LogInCommand { get; }

        public LoginViewModel()
        {
            LogInCommand = AsyncCommand.Create(LogIn);
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value; 
                OnPropertyChanged();
            }
        }

        public string UserPassword
        {
            get => _userPassword;
            set
            {
                _userPassword = value;
                OnPropertyChanged();
            }
        }

        private async Task LogIn()
        {
            //if (UserName == "monnaanna" && UserPassword == "12345")
            {
                Store.CreateOrGet<MenuViewModel>().LoadData();
                await Task.Delay(1);

                //Store.CreateOrGet<PriceViewModel>().LoadData();

                Navigator.Instance.NavigationService.Navigate(new PreviewOperationView());
            }
        }
    }
}
