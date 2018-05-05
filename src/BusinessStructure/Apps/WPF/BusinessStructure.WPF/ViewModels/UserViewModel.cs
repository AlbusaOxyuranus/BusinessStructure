using BlackBee.Toolkit.Base;

namespace BusinessStructure.Vms
{
    public class UserViewModel: ViewModelBase
    {
        private string _userName;
        private string _userPassword;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string UserPassword
        {
            get => _userPassword;
            set
            {
                _userPassword = value;
                OnPropertyChanged();
            }
        }
    }
}
