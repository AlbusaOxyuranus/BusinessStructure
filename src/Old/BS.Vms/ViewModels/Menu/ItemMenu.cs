using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Geliada.DesktopApp.ViewModels.Menu
{
    public class ItemMenu
    {
        public ICommand GetPageCommmand { get; set; }
        public string Name { get; set; }
    }
}
