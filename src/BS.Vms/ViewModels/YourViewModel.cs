using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BlackBee.Toolkit.Base;
using BlackBee.Toolkit.Base.Annotations;

namespace BS.Vms.ViewModels
{
    public class YourViewModel : ViewModelBase
    {
        private ObservableCollection<string> countDownElements = new ObservableCollection<string> { "1s", "2s", "3s", "4s", "5s", "6s", "7s", "8s", "9s", "10s" };
        private string selectedItem;

        public ObservableCollection<string> CountDownElements
        {
            get { return countDownElements; }
            set
            {
                if (Equals(value, countDownElements)) return;
                countDownElements = value;
                OnPropertyChanged();
            }
        }

        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (value == selectedItem) return;
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator] // remove if you don't have R#
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
