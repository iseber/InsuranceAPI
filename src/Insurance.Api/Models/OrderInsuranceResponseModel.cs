using System;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.Api.Models
{
    public class OrderInsuranceResponseModel
    {
        public List<InsuranceResponseModel> InsuranceResponseModels { get; set; }
        
        public float SumOfOrderInsurance => InsuranceResponseModels.Sum(x => x.InsuranceValue) + OrderSurcharge;

        public float OrderSurcharge { get; set; }
        
        public OrderInsuranceResponseModel()
        {
            InsuranceResponseModels = new List<InsuranceResponseModel>();
        }
    }
}