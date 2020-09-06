namespace Insurance.Api.Models
{
    public class ProductResponseModel
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public bool ProductTypeHasInsurance { get; set; }
        public float SalesPrice { get; set; }
    }
}