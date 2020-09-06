namespace Insurance.Api.Domain
{
    public class Insurance
    {
        private int productId;
        private float salesPrice;
        private bool productTypeHasInsurance;
        private string productTypeName;
        private float insuranceValue;
        
        internal Insurance()
        {
            
        }

        private Insurance(int productId, float salesPrice, bool productTypeHasInsurance, string productTypeName)
        {
            this.productId = productId;
            this.salesPrice = salesPrice;
            this.productTypeHasInsurance = productTypeHasInsurance;
            this.productTypeName = productTypeName;
            this.insuranceValue = 0;
        }

        public static Insurance New(int productId, float salesPrice, bool productTypeHasInsurance, string productTypeName)
        {
            return new Insurance(productId, salesPrice, productTypeHasInsurance, productTypeName);
        }

        public int ProductId => productId;
        public float InsuranceValue => insuranceValue;
        
        public void CalculateInsuranceValue(float insuranceValue)
        {
            if(!this.productTypeHasInsurance) return;
            
            this.insuranceValue += insuranceValue;
            
            // if (this.salesPrice < 500)
            // {
            //     if (this.productTypeName == "Laptops")
            //         this.insuranceValue += 500;
            //     else
            //         this.insuranceValue = 0;
            // }
            // else
            // {
            //     if (this.salesPrice > 500 && this.salesPrice < 2000)
            //         if (this.productTypeHasInsurance)
            //             insuranceValue += 1000;
            //     if (this.salesPrice >= 2000)
            //         if (this.productTypeHasInsurance)
            //             insuranceValue += 2000;
            //     if (this.productTypeName == "Laptops" || this.productTypeName == "Smartphones" && this.productTypeHasInsurance)
            //         this.insuranceValue += 500;
            // }
        }

        public void CalculateSurcharge(float surchargeValue)
        {
            this.insuranceValue += surchargeValue;
        }
    }
}