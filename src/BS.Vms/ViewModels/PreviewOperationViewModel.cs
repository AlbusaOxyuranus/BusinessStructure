using System;
using System.Threading.Tasks;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Commands;
using BS.WPF.Views.Pages;

namespace BS.Vms.ViewModels
{
    public class PreviewOperationViewModel : ViewModelBase
    {
        public PreviewOperationViewModel()
        {
            GetPriceAsyncCommand = AsyncCommand.Create(GetPriceAsync);
            GetContactsAsyncCommand = AsyncCommand.Create(GetContactsAsync);
            GetClientsAsyncCommand = AsyncCommand.Create(GetClientsAsync);
        }

        public IAsyncCommand GetPriceAsyncCommand { get; }

        public IAsyncCommand GetContactsAsyncCommand { get; }
        public IAsyncCommand GetClientsAsyncCommand { get; }

        private async Task GetClientsAsync()
        {
            await Task.Delay(1);

            //await Store.CreateOrGet<OrderViewModel>().LoadData();

            Navigator.Instance.NavigationService.Navigate(new ClientView());
        }

        private async Task GetContactsAsync()
        {
            await Task.Delay(1);

            Store.CreateOrGet<OrderViewModel>().LoadData();

            Navigator.Instance.NavigationService.Navigate(new OrderPage());
        }

        private async Task GetPriceAsync()
        {
            await Task.Delay(1);

            Store.CreateOrGet<PriceViewModel>().LoadData();

            Navigator.Instance.NavigationService.Navigate(new PriceView());
        }
    }
}