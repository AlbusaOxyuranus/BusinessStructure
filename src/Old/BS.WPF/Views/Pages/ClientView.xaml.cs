﻿using System.Windows;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Controls;
using BS.Vms.ViewModels;

namespace BS.WPF.Views.Pages
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
            DataContext = Store.CreateOrGet<BS.Vms.ViewModels.ClientViewModel>();
        }

        private void ClientView_OnLoaded(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}