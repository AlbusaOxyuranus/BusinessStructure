using System;
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
        public string PriceUsd { get; set; }
        public int? WeightVolumeUnits { get; set; }

        public string Price_Byn
        {
            get
            {
                if (WeightVolumeUnits != null) return (Math.Round(double.Parse(PriceUsd) * 2.1,2).ToString("#.#0"));
                return 0.ToString("#.00");
            }
        }
        public string Price_Byn_unit
        {
            get
            {
                if (WeightVolumeUnits != null) return (Math.Round(double.Parse(PriceUsd) / WeightVolumeUnits.Value * 2.1,2)).ToString("#.#0");
                return 0.ToString("#.00");
            }
        }
    }
}
