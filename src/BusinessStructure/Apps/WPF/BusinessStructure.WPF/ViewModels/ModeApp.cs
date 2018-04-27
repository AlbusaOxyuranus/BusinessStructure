using System;
using System.Collections.Generic;
using System.Text;
using BusinessStructure.Bc;

namespace BusinessStructure.Vms
{
    public class ModeApp : IModeApp
    {
        #region Implementation Singleton

        private static volatile ModeApp instance;
        private static object syncRoot = new Object();

        private ModeApp()
        {
        }
        public static ModeApp Instance
        {
            get
            {

                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ModeApp();

                        }
                    }
                }

                return instance;
            }
        }
        #endregion

        public string AdressConnection => Adress[ConnectionBase];

        public ConnectionBase ConnectionBase { get { return ConnectionBase.MonnaAnnaSite; } }

        private static readonly Dictionary<ConnectionBase, string> Adress = new Dictionary
            <ConnectionBase, string>()
            {
                {ConnectionBase.MonnaAnnaSite, "http://geliada-rest-api.monnaanna.by/"},
                //{ConnectionBase.MonnaAnnaSite, "http://geliada-rest-api.monnaanna.by/"}
            };


    }
}
