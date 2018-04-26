using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.HockeyApp;

namespace BusinessStructure.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Microsoft.HockeyApp.HockeyClient.Current.Configure("95661e6c00a1459bad4dd5f8830d7663 ");
        }
    }
}
