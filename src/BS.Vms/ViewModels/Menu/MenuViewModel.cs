using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Commands;


namespace Geliada.DesktopApp.ViewModels.Menu
{
    public class MenuViewModel : ViewModelBase
    {
        public ObservableCollection<GategoryViewModel> Gategoryes { get; }


        public MenuViewModel()
        {
            Gategoryes = new ObservableCollection<GategoryViewModel>()
            {
                //new GategoryViewModel() {Name = "Бизнес", Categoryes = CategoryExtension.Bookkeeping},
                //new GategoryViewModel() {Name = "Документооборот", Categoryes = CategoryExtension.Documents},
                //new GategoryViewModel() {Name = "Шаблоны документов", Categoryes = CategoryExtension.Templates},
                //new GategoryViewModel() {Name = "Счет-фактуры", Categoryes = CategoryExtension.Invoices},
                //new GategoryViewModel() {Name = "База клиентов", Categoryes = CategoryExtension.BaseClient},
                //new GategoryViewModel() {Name = "Рассылка", Categoryes = CategoryExtension.Newsletter},
                //new GategoryViewModel() {Name = "Уведомления", Categoryes = CategoryExtension.Notifications},
                new GategoryViewModel() {Name = "Интеграция с Web-сайтами", Categoryes = CategoryExtension.Site}
                //new GategoryViewModel() {Name = "Отчетность", Categoryes = CategoryExtension.Reports},
            };

            //PageSource = new Uri("Pages/MenuPageView.xaml", UriKind.Relative);

        }

        public void LoadData()
        {
            foreach (var categ in Gategoryes)
            {
                categ.Items.Add(new ItemMenu()
                {
                    Name = "Формирование прайс-листов",
                    GetPageCommmand = new RelayCommand(() => { Debug.WriteLine("Test"); })
                });
            }
            //PluginDir.Instance.GetPlugins();

                //foreach (var categ in Gategoryes)
                //{
                //    //foreach (
                //    //    var plug in
                //    //    PluginDir.Instance.Plugins.Where(x => x.Value.CategoryExtensionName == categ.Categoryes).ToList())
                //    //{
                //    //    //if (plug.Key.ToString().Contains("прайс"))
                //    //    //    categ.Items.Add(new ItemMenu()
                //    //    //    {
                //    //    //        Name = plug.Key,
                //    //    //        GetPageCommmand = new RelayCommand(() =>
                //    //    //        {
                //    //    //            //PageSource = new Uri("Pages/MenuPageView.xaml", UriKind.Relative);
                //    //    //            Navigator.Instance.NavigationService.Navigate(new MenuPageView());
                //    //    //        })
                //    //    //    });
                //    //    //if (plug.Key.ToString().Contains("НБРБ"))
                //    //        categ.Items.Add(new ItemMenu()
                //    //        {
                //    //            Name = plug.Key,
                //    //            GetPageCommmand = new RelayCommand(() =>
                //    //            {
                //    //                //PageSource = new Uri("Pages/MenuPageView.xaml", UriKind.Relative);
                //    //                Navigator.Instance.NavigationService.Navigate(plug.Value.UserControl);
                //    //            })
                //    //        }
                //    //    );
                //    //    //else
                //    //    //{
                //    //    //    categ.Items.Add(new ItemMenu()
                //    //    //    {
                //    //    //        Name = plug.Key,
                //    //    //        GetPageCommmand = new RelayCommand(() =>
                //    //    //        {
                //    //    //            Navigator.Instance.NavigationService.Navigate(new StartPageView()); 
                //    //    //        })
                //    //    //    });
                //    //    //}
                //    //}

                //}
        }

        public Uri PageSource { get; set; }
    }

}
