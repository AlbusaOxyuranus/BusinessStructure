using System;
using System.Windows.Controls;


namespace BusinessStructure
{
    public class Navigator
    {
        #region Implementation Singleton

        private static volatile Navigator _instance;
        private static object syncRoot = new Object();

        private Navigator()
        {
        }
        public static Navigator Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Navigator();

                        }
                    }
                }

                return _instance;
            }
        }

        public Frame NavigationService { get; set; }

        #endregion
    }
}