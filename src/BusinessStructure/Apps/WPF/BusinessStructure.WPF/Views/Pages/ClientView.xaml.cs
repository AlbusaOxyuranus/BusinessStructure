using System.Windows;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Controls;
using BusinessStructure.Vms.ViewModels;

namespace BusinessStructure.WPF.Views.Pages
{
    /// <summary>
    ///     Логика взаимодействия для ClientView.xaml
    /// </summary>
    public partial class ClientView : BusyIndicatorPage
    {
        public ClientView()
        {
            InitializeComponent();
            CreateIndicate(MainGrid);
            DataContext = Store.CreateOrGet<BusinessStructure.Vms.ViewModels.ClientViewModel>();
        }

        private void ClientView_OnLoaded(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}