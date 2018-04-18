using System;
using System.Windows;
using System.Windows.Controls;
using BlackBee.Toolkit.Base;
using Geliada.DesktopApp.ViewModels.Menu;

namespace BS.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigator.Instance.NavigationService = FrameNavigator;
            DataContext = Store.CreateOrGet<MenuViewModel>();
        }

        //private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    var model = new MenuViewModel();
        //    model.LoadData();
        //    DataContext = model;
        //}
    }

}
