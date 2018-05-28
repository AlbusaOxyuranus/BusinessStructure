using System.ServiceModel;

namespace BS.AuthenticationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IManagmentService" in both code and config file together.
    [ServiceContract]
    public interface IManagmentService
    {
        [OperationContract]
        void DoWork();
    }
}
