using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BS.AuthenticationService
{[DataContract]
    public enum UserInfoErrorEnum
    {
       [EnumMember]
        Locked
    }
    public class UserInfoError:List<UserInfoErrorEnum>
    {
    }
}