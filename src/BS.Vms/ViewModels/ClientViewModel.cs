using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Commands;
using BS.Vms.JShoping.ImExPrice.BAL;
using BS.Vms.ViewModels.price;
using BS.WPF.Views.Pages;

namespace BS.Vms.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public string Street { get; set; }

        private ObservableCollection<ClientViewModel> _list;
        
        public ClientViewModel()
        {
            _list = new ObservableCollection<ClientViewModel>();
            NavigateMainCommand = AsyncCommand.Create(NavigateMain);
        }
        public IAsyncCommand NavigateMainCommand { get; private set; }
        private async Task NavigateMain()
        {
            await Task.Delay(1);

            Navigator.Instance.NavigationService.Navigate(new PreviewOperationView());
        }

        public async Task LoadData()
        {
            Store.CreateOrGet<ClientViewModel>().BussinessProcess = true;
            Store.CreateOrGet<ClientViewModel>().List = await GetClients();
            Store.CreateOrGet<ClientViewModel>().BussinessProcess = false;
        }
        public static async Task<ObservableCollection<ClientViewModel>> GetClients()
        {
            var resultLIst = new ObservableCollection<ClientViewModel>();
            using (var bc = new ImExPriceBusinessContext(ModeApp.Instance))
            {
                var res = await bc.GetClientsAsync();
                //await TaskHelper.RunAsync(() =>
                //{
                    foreach (var r in res)
                    {
                        resultLIst.Add(new ClientViewModel()
                        {
                            LastName = r.LastName,
                            FirstName = r.FirstName,
                            Email = r.Email,
                            City = r.City,
                            Street = r.Street,
                            Phone = r.Phone

                        });
                    }
                    
                //});
            }

            return resultLIst;
        }

        public ObservableCollection<ClientViewModel> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
    }
}