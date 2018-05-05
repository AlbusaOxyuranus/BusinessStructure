﻿using System;
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
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public RequestResult<UserResult,UserError> Login(string email, string password)
        {
            if(email!= "nadski@yandex.by" || password!="1")
            {
                return new RequestResult<UserResult,UserError>(null,new UserErrorList().Single(x=>x.UserErrorEnum==UserErrorEnum.NotFound));
            }
            return new RequestResult<UserResult, UserError>(new UserResult(){FullUserName = "Сугак Надежда Анатольевна"});
        }

        public void ResetPassword(string email)
        {
            throw new NotImplementedException();
        }
    }
}
