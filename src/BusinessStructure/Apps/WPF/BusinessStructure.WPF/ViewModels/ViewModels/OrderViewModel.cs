using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Commands;
using BusinessStructure.Vms.JShoping.ImExPrice.BAL;
using BusinessStructure.WPF.Views.Pages;

namespace BusinessStructure.Vms.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private ObservableCollection<OrderViewModel> _list;

        public OrderViewModel()
        {
            _list = new ObservableCollection<OrderViewModel>();
            NavigateMainCommand = AsyncCommand.Create(NavigateMain);
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public string Street { get; set; }
        public IAsyncCommand NavigateMainCommand { get; }

        public ObservableCollection<OrderViewModel> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        private async Task NavigateMain()
        {
            await Task.Delay(1);

            Navigator.Instance.NavigationService.Navigate(new PreviewOperationView());
        }

        public async Task LoadData()
        {
            Store.CreateOrGet<OrderViewModel>().BussinessProcess = true;
            Store.CreateOrGet<OrderViewModel>().List = await GetClients();
            Store.CreateOrGet<OrderViewModel>().BussinessProcess = false;
        }

        public static async Task<ObservableCollection<OrderViewModel>> GetClients()
        {
            var resultLIst = new ObservableCollection<OrderViewModel>();
            using (var bc = new ImExPriceBusinessContext(ModeApp.Instance))
            {
                var res = await bc.GetClientsAsync();
                //await TaskHelper.RunAsync(() =>
                //{
                foreach (var r in res)
                    resultLIst.Add(new OrderViewModel
                    {
                        LastName = r.LastName,
                        FirstName = r.FirstName,
                        Email = r.Email,
                        City = r.City,
                        Street = r.Street,
                        Phone = r.Phone
                    });

                //});
            }

            return resultLIst;
        }
    }
}