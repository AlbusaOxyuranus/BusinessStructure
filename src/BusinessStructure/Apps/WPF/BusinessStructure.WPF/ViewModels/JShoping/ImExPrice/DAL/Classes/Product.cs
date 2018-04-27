using System.Runtime.Serialization;

namespace BusinessStructure.Vms.JShoping.ImExPrice.DAL.Classes
{
    [DataContract]
    public class Product
    {
        [DataMember(Name = "product_id")] public int ProductId { get; set; }

        [DataMember(Name = "product_ean")] public string ProductEan { get; set; }

        [DataMember(Name = "name_ru-RU")] public string Name { get; set; }

        [DataMember(Name = "product_quantity")]
        public int? ProductQuantity { get; set; }

        [DataMember(Name = "product_price")] public double? ProductPrice { get; set; }

        [DataMember(Name = "weight_volume_units")]
        public int? WeightVolumeUnits { get; set; }

        [DataMember(Name = "product_publish")] public int ProductPublish { get; set; }
        [DataMember(Name = "image")] public string Image { get; set; }

        [DataMember(Name = "extra_field_1")] public string ExtraField1 { get; set; }

        [DataMember(Name = "extra_field_2")] public string ExtraField2 { get; set; }
    }
}