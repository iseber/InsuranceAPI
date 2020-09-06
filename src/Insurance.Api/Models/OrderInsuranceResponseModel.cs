using System;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.Api.Models
{
    public class OrderInsuranceResponseModel
    {
        public List<InsuranceResponseModel> InsuranceResponseModels { get; set; }

        public Dictionary<int, int> Quantity => InsuranceResponseModels.ToDictionary(x => x.ProductId,
            x => InsuranceResponseModels.Select(x=>x.ProductId).Count());

        public float SumOfOrderInsurance => InsuranceResponseModels.Sum(x => x.InsuranceValue);

        public OrderInsuranceResponseModel()
        {
            InsuranceResponseModels = new List<InsuranceResponseModel>();
        }
    }
}