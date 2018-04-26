using System.Windows;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Controls;
using BS.Vms.ViewModels;

namespace BS.WPF.Views.Pages
{
    /// <summary>
    ///     Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : BusyIndicatorPage
    {
        public OrderPage()
        {
            InitializeComponent();
            CreateIndicate(MainGrid);
            DataContext = Store.CreateOrGet<OrderViewModel>();
        }

        private async void OrderPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await Store.CreateOrGet<OrderViewModel>().LoadData();
        }
    }
}