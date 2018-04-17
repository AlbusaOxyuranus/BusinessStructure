using System.Drawing;
using BlackBee.Toolkit.Base;

namespace BS.Vms.ViewModels.price
{
    public class ProductViewModel:ViewModelBase
    {
        private string _name;
        private int _id;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string ImageBit { get; set; }
    }
}
