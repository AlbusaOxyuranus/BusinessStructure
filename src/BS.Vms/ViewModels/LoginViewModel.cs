using BlackBee.Toolkit.Base;
using System.Windows.Input;
using BlackBee.Toolkit.Commands;
using System.Threading.Tasks;

namespace BS.Vms.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        public ICommand LogInCommand { get; }

        public LoginViewModel()
        {
            LogInCommand = AsyncCommand.Create(LogIn);
        }

        private async Task LogIn()
        {
            await Task.Delay(100);
        }
    }
}
