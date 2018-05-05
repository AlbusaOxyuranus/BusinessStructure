using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BS.AuthenticationService
{
    [DataContract]
    public enum UserErrorEnum
    {
        [EnumMember] NotFound = 0,
        [EnumMember] UserLocked = 1
    }

    public class UserErrorList:List<UserError>
    {
        public UserErrorList()
        {
            Add(new UserError(){UserErrorEnum = UserErrorEnum.NotFound,MessageError = "Пользователь не найден"});
            Add(new UserError() { UserErrorEnum = UserErrorEnum.UserLocked, MessageError = "Пользователь заблокирован системой" });
        }
    }

    [DataContract]
    public class UserError
    {
        [DataMember] public UserErrorEnum UserErrorEnum { get; set; }
        [DataMember] public string MessageError { get; set; }
    }
}