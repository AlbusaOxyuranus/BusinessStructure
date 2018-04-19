using System.Threading.Tasks;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Commands;
using BS.Vms.JShoping.ImExPrice.DAL.Classes;
using BS.WPF.Views.Pages;

namespace BS.Vms.ViewModels
{
    public class PreviewOperationViewModel:ViewModelBase
    {
        public IAsyncCommand GetPriceAsyncCommand { get; }

        public IAsyncCommand GetContactsAsyncCommand { get; }


        public PreviewOperationViewModel()
        {
            GetPriceAsyncCommand = AsyncCommand.Create(GetPriceAsync);
            GetContactsAsyncCommand = AsyncCommand.Create(GetContactsAsync);
        }

        private async Task GetContactsAsync()
        {
            await Task.Delay(1);

            await Store.CreateOrGet<ClientViewModel>().LoadData();

            Navigator.Instance.NavigationService.Navigate(new ClientPage());
        }

        private async Task GetPriceAsync()
        {
            await Task.Delay(1);

            Store.CreateOrGet<PriceViewModel>().LoadData();

            Navigator.Instance.NavigationService.Navigate(new PriceView());
        }
    }
}
