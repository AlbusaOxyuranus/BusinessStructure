using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlackBee.Toolkit.Base;
using BS.Vms.ViewModels;

namespace BS.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PreviewOperationView.xaml
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
