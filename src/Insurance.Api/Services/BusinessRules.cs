using System;
using System.Net.Http;
using System.Threading.Tasks;
using Insurance.Api.Config;
using Insurance.Api.Exceptions;
using Insurance.Api.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace Insurance.Api.Services
{
    public class BusinessRules : IBusinessRules
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger = Log.ForContext<BusinessRules>();
        
        public BusinessRules(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        
        public async Task<ProductResponseModel> GetProductTypeAndSalesPrice(int productId)
        {
            try
            {
                var client = new HttpClient {BaseAddress = new Uri(_appSettings.ProductApi)};

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
            catch (ProductAPIRequestUrlException ex)
            {
                _logger.Error($"The request url is invalid: { _appSettings?.ProductApi }");
                throw new ProductAPIRequestUrlException($"The request url is invalid: { _appSettings?.ProductApi }");
            }
            catch (HttpRequestException ex)
            {
                _logger.Error($"Timeout error from Product API for product: { productId }");
                throw new ProductAPITimeoutException($"Timeout error from Product API. Ex: {ex.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Unexpected error from Product API product: { productId }");
                throw new ProductAPIException($"Unexpected error from Product API. Ex: {ex.ToString()}");
            }
        }
    }
}