using System.Collections.Generic;

namespace Insurance.Api.Models
{
    public class UploadSurchargesRequestModel
    {
        public IList<ProductSurcharges> ProductSurcharges { get; set; }
    }

    public class ProductSurcharges
    {
        public int ProductTypeId { get; set; }
        public float Surcharge { get; set; }
    }
}