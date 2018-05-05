using System;
using System.Runtime.Serialization;

namespace BS.AuthenticationService
{
    [DataContract]
    [KnownType("GetTypes")]
    public class RequestResult<TData, TError>
        where TData : class, new()
        where TError : class, new()

    {
        public RequestResult(TData data = null, TError error = null)
        {
            Data = data;
            Error = error;
        }

        [DataMember] public TData Data { get; set; }

        [DataMember] public TError Error { get; set; }


        private static Type[] GetTypes()
        {
            return new[] {typeof(RequestResult<TData, TError>)};
        }
    }
}