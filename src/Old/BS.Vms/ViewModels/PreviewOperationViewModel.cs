using System.Threading.Tasks;
using System.Windows.Input;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Commands;
using BS.WPF.Views.Pages;

namespace BS.Vms.ViewModels
{
    public class PreviewOperationViewModel : ViewModelBase
    {
        public PreviewOperationViewModel()
        {
            GetPriceAsyncCommand = new RelayCommand(PriceViewNavigated);
            GetContactsAsyncCommand = new RelayCommand(ContactsViewNavigated);
            GetClientsAsyncCommand = new RelayCommand(ClientViewNavigated);
        }

        public ICommand GetPriceAsyncCommand { get; }

        public ICommand GetContactsAsyncCommand { get; }
        public ICommand GetClientsAsyncCommand { get; }

        private void ClientViewNavigated()
        {
            Navigator.Instance.NavigationService.Navigate(new ClientView());
        }

        private void ContactsViewNavigated()
        {
            Navigator.Instance.NavigationService.Navigate(new OrderPage());
        }

        private void PriceViewNavigated()
        {
            Navigator.Instance.NavigationService.Navigate(new PriceView());
        }
    }
}