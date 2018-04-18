using System.Collections.Generic;
using System.Windows.Input;
using BlackBee.Toolkit.Base;


namespace Geliada.DesktopApp.ViewModels.Menu
{
    public class GategoryViewModel:ViewModelBase
    {
        

        public CategoryExtension Categoryes { get; set; }
        public string Name { get; set; }
        public List<ItemMenu> Items { get; set; }

        public GategoryViewModel()
        {
            Items=new List<ItemMenu>();
        }
    }
}
