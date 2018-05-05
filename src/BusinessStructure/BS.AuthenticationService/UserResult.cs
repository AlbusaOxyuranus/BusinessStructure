using System.Runtime.Serialization;

namespace BS.AuthenticationService
{
    [DataContract]
    public class UserResult
    {
        [DataMember] public string FullUserName { get; set; }
    }
}