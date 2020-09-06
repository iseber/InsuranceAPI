using System;
using System.Net.Http;
using System.Threading.Tasks;
using Insurance.Api.Config;
using Insurance.Api.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Insurance.Api.Services
{
    public class BusinessRules : IBusinessRules
    {
        private readonly AppSettings appSettings;
        
        public BusinessRules(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }
        
        public async Task<ProductResponseModel> GetProductTypeAndSalesPrice(int productId)
        {
            var client = new HttpClient{ BaseAddress = new Uri(appSettings.ProductApi)};
            
            var productTypesJson = await client.GetAsync("/product_types");
            var productTypesContent = await productTypesJson.Content.ReadAsStringAsync();
            var collection = JsonConvert.DeserializeObject<dynamic>(productTypesContent);

            var productsJson = await client.GetAsync(string.Format("/products/{0:G}", productId));
            var productsContent = await productsJson.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<dynamic>(productsContent);

            var productResponseModel = new ProductResponseModel();

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].id == product.productTypeId && collection[i].canBeInsured == true)
                {
                    productResponseModel.ProductTypeId = collection[i].id;
                    productResponseModel.ProductTypeName = collection[i].name;
                    productResponseModel.ProductTypeHasInsurance = true;
                    productResponseModel.SalesPrice = product.salesPrice;
                }
            }

            return productResponseModel;
        }
    }
}