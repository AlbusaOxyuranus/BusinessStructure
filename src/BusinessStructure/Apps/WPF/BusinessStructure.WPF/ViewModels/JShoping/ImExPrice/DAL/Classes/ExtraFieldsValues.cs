using System.Runtime.Serialization;

namespace BusinessStructure.Vms.JShoping.ImExPrice.DAL.Classes
{
    [DataContract]
    public class ExtraFieldsValues
    {
        [DataMember(Name = "id")] public int Id { get; set; }

        [DataMember(Name = "field_id")] public int FieldId { get; set; }

        [DataMember(Name = "ordering")] public int Ordering { get; set; }

        [DataMember(Name = "name_ru-RU")] public string NameRu { get; set; }
    }
}