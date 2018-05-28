using System;
using System.Collections.Generic;
using System.Linq;

namespace BS.AuthenticationService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class AuthentificationManagement : IAuthentificationManagement
    {
        public string GetData(int value)
        {
            return $"You entered: {value}";
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null) throw new ArgumentNullException(nameof(composite));
            if (composite.BoolValue) composite.StringValue += "Suffix";
            return composite;
        }

        public RequestResult<UserResult, UserError> Login(string login, string password)
        {
            var dataDb = new DataDb();
            
            if(dataDb.Any(x=>x.Login == login && x.Password==password))
                return new RequestResult<UserResult, UserError>(null,new UserErrorList().Single(x => x.UserErrorEnum == UserErrorEnum.NotFound));

            var result = dataDb.Single(x => x.Login == login && x.Password == password);
            return new RequestResult<UserResult, UserError>(new UserResult
            {
                FullUserName = result.FullUserName
            });
            
            //if (login != "nadski@yandex.by" || password != "1")
            //    return new RequestResult<UserResult, UserError>(null,
            //        new UserErrorList().Single(x => x.UserErrorEnum == UserErrorEnum.NotFound));
            //return new RequestResult<UserResult, UserError>(new UserResult
            //{
            //    FullUserName = "Сугак Надежда Анатольевна"
            //});
        }

        public RequestResult<UserInfoResult, UserInfoError> GetUserInfo()
        {
            throw new NotImplementedException();
        }

        public object ResetPassword(string email)
        {
            throw new NotImplementedException();
        }
    }

    public class User
    {
        public string Login { get; set; }
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string FullUserName { get; set; }
    }
    //public class UserInfo{
    //    public Guid Id { get; set; }
    //    public 
    //}
    public class DataDb : List<User>
    {
        public DataDb()
        {
            Add(new User
            {
                Id = Guid.NewGuid(),
                Login = "monnaanna.by",
                Password = "12345",
                FullUserName = "Сугак Надежда Анатольевна"
            });
            Add(new User
            {
                Id = Guid.NewGuid(),
                Login = "fastmebel.by",
                Password = "12345",
                FullUserName = "Сугак Олег Анатольевна"
            });
        }
    }
}