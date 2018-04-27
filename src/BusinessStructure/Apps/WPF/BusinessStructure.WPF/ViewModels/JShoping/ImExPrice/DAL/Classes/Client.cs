using System.Runtime.Serialization;

namespace BusinessStructure.Vms.JShoping.ImExPrice.DAL.Classes
{
    [DataContract]
    public class Client
    {
        [DataMember(Name = "l_name")] public string LastName { get; set; }
        [DataMember(Name = "f_name")] public string FirstName { get; set; }
        [DataMember(Name = "email")] public string Email { get; set; }
        [DataMember(Name = "street")] public string Street { get; set; }
        [DataMember(Name = "city")] public string City { get; set; }
        [DataMember(Name = "phone")] public string Phone { get; set; }
    }
}