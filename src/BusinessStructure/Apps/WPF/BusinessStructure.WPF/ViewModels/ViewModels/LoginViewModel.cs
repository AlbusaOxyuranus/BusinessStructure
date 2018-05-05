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
        public UserViewModel UserViewModel
        {
            get => _userViewModel;
            set
            {
                _userViewModel = value;
                OnPropertyChanged();
            }
        }

        public EventHandler ClearPasswordEventHandler = null;
       
        private UserViewModel _userViewModel;
        public IAsyncCommand LogInCommand { get; }

        public LoginViewModel()
        {
            UserViewModel = Store.CreateOrGet<UserViewModel>();
            LogInCommand = AsyncCommand.Create(LogIn);
        }

       

        private async Task LogIn()
        {
            if (!string.IsNullOrEmpty(UserViewModel.UserName) && !string.IsNullOrWhiteSpace(UserViewModel.UserName)
                                                && !string.IsNullOrEmpty(UserViewModel.UserPassword) &&
                                                !string.IsNullOrWhiteSpace(UserViewModel.UserPassword))
            {
                var vClient = new AuthentificationManagementClient();
                var result = await vClient.LoginAsync(UserViewModel.UserName, UserViewModel.UserPassword);
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