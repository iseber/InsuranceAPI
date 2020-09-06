namespace Insurance.Api.Domain
{
    public class Insurance
    {
        private int productId;
        private float salesPrice;
        private bool productTypeHasInsurance;
        private int productTypeId;
        private float insuranceValue;
        
        internal Insurance()
        {
            
        }

        private Insurance(int productId, float salesPrice, bool productTypeHasInsurance, int productTypeId)
        {
            this.productId = productId;
            this.salesPrice = salesPrice;
            this.productTypeHasInsurance = productTypeHasInsurance;
            this.productTypeId = productTypeId;
            this.insuranceValue = 0;
        }

        public static Insurance New(int productId, float salesPrice, bool productTypeHasInsurance, int productTypeId)
        {
            return new Insurance(productId, salesPrice, productTypeHasInsurance, productTypeId);
        }

        public int ProductId => productId;
        public int ProductTypeId => productTypeId;
        public float InsuranceValue => insuranceValue;
        
        public void CalculateInsuranceValue(float insuranceValue)
        {
            if(!this.productTypeHasInsurance) return;
            
            this.insuranceValue += insuranceValue;
        }

        public void CalculateSurcharge(float surchargeValue)
        {
            this.insuranceValue += surchargeValue;
        }
    }
}