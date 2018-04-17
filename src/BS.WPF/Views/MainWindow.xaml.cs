using System;
using System.Windows;
using System.Windows.Controls;

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
        }
    }

}
