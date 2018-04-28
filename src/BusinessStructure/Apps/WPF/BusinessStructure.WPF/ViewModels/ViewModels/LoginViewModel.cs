using System;
using BlackBee.Toolkit.Base;
using System.Windows.Input;
using BlackBee.Toolkit.Commands;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BusinessStructure.WPF.Views.Pages;
using Geliada.DesktopApp.ViewModels.Menu;
using Microsoft.Office.Interop.Excel;

namespace BusinessStructure.Vms.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        public EventHandler ClearPasswordEventHandler=null;
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
            if (UserName == "monna" && UserPassword == "1")
            {
                Store.CreateOrGet<MenuViewModel>().LoadData();
                await Task.Delay(1);

                //Store.CreateOrGet<PriceViewModel>().LoadData();

                Navigator.Instance.NavigationService.Navigate(new PreviewOperationView());
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль","Ошибка",MessageBoxButton.OK,MessageBoxImage.Information);
                UserName=String.Empty;
               UserPassword=String.Empty;
                
            }
        }
    }
}
