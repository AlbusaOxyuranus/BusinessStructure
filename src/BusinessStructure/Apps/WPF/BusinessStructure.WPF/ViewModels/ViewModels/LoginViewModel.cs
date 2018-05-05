using System;
using System.Threading.Tasks;
using System.Windows;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Commands;
using BusinessStructure.WPF.AService;
using BusinessStructure.WPF.Views.Pages;

namespace BusinessStructure.Vms.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public EventHandler ClearPasswordEventHandler = null;
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
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrWhiteSpace(UserName)
                                                && !string.IsNullOrEmpty(UserPassword) &&
                                                !string.IsNullOrWhiteSpace(UserPassword))
            {
                var vClient = new AuthentificationManagementClient();
                var result = await vClient.LoginAsync(UserName, UserPassword);
                if (result.Error != null)
                {
                    MessageBox.Show(result.Error.MessageError, "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                Navigator.Instance.NavigationService.Navigate(new PreviewOperationView());
            }
            else
            {
                MessageBox.Show("Чтобы осуществить вход в систему необходимо ввести имя пользователя и пароль",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //if (UserName == "monna" && UserPassword == "1")
            //{
            //    Store.CreateOrGet<MenuViewModel>().LoadData();
            //    await Task.Delay(1);

            //    //Store.CreateOrGet<PriceViewModel>().LoadData();

            //    Navigator.Instance.NavigationService.Navigate(new PreviewOperationView());
            //}
            //else
            //{
            //    MessageBox.Show("Неверное имя пользователя или пароль","Ошибка",MessageBoxButton.OK,MessageBoxImage.Information);
            //    UserName=String.Empty;
            //   UserPassword=String.Empty;

            //}
        }
    }
}