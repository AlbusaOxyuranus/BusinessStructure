using System;
using System.Text;
using BlackBee.Toolkit.Rest.EasyData;

namespace BS.Bc
{
    public class BussinessContext : IBussinessContext
    {
        public IDataContext DataContext { get; protected set; }
        


        public BussinessContext(IModeApp modeApp)
        {
            DataContext = new DataContext(modeApp.ConnectionBase.ToString(), modeApp.AdressConnection, Encoding.UTF8); 
            
        }

        #region implementation IDisposable

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
                return;

            DataContext?.Dispose();
        }

        #endregion

       
    }

}

