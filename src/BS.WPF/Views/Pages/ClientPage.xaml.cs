using System.Windows.Controls;
using BlackBee.Toolkit.Base;
using BS.Vms.ViewModels;

namespace BS.WPF.Views.Pages
{
    /// <summary>
    ///     Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            DataContext = Store.CreateOrGet<ClientViewModel>();
        }
    }
}