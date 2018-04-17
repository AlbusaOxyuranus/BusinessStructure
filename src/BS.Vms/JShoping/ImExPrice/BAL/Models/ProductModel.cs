namespace BS.Vms.JShoping.ImExPrice.BAL.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductEan { get; set; }
        public string Name { get; set; }
        public double? ProductPrice { get; set; }
        public int? ProductQuantity { get; set; }
        public int? WeightVolumeUnits { get; set; }
        public int ProductPublish { get; set; }
        public string ImageUrl { get; set; }
    }
}