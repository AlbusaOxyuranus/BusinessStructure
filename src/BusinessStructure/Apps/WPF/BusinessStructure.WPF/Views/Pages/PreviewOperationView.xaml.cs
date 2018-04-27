using System.Windows.Controls;
using BlackBee.Toolkit.Base;
using BusinessStructure.Vms.ViewModels;

namespace BusinessStructure.WPF.Views.Pages
{
    /// <summary>
    ///     Логика взаимодействия для PreviewOperationView.xaml
    /// </summary>
    public partial class PreviewOperationView : Page
    {
        public PreviewOperationView()
        {
            InitializeComponent();
            DataContext = Store.CreateOrGet<PreviewOperationViewModel>();
        }
    }
}