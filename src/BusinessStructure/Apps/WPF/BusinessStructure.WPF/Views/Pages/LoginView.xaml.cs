using System;
using System.Windows;
using System.Windows.Controls;
using BlackBee.Toolkit.Base;
using BusinessStructure.Vms.ViewModels;

namespace BusinessStructure.WPF.Views.Pages
{
    /// <summary>
    ///     Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = Store.CreateOrGet<LoginViewModel>();
           
        }


        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { Store.CreateOrGet<LoginViewModel>().UserPassword = ((PasswordBox)sender).Password; }
        }


    }
}