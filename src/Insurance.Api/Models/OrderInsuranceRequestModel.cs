using System.Collections.Generic;

namespace Insurance.Api.Models
{
    public class OrderInsuranceRequestModel
    {
        public List<OrderRequestModel> Products { get; set; }
    }

    public class OrderRequestModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}