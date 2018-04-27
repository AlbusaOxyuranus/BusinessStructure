using System.Windows;
using System.Windows.Controls;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Controls;
using BusinessStructure.Vms.ViewModels;

namespace BusinessStructure.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PriceView.xaml
    /// </summary>
    public partial class PriceView : BusyIndicatorPage
    {
        public PriceView()
        {
            InitializeComponent();
            CreateIndicate(MainGrid);
            DataContext = Store.CreateOrGet<PriceViewModel>();
           
        }

        private async void PriceView_OnLoaded(object sender, RoutedEventArgs e)
        {
            await Store.CreateOrGet<PriceViewModel>().LoadData();
        }
    }
}
