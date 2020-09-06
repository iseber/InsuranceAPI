using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurance.Api.Models;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    public class HomeController: Controller
    {
        private readonly IInsuranceDomainService _insuranceDomainService;
        public HomeController(IInsuranceDomainService insuranceDomainService)
        {
            _insuranceDomainService = insuranceDomainService;
        }
        
        [HttpPost]
        [Route("api/insurance/product")]
        public async Task<IActionResult> CalculateInsurance([FromBody] InsuranceRequestModel requestModel)
        {
            var insurance = await _insuranceDomainService.GetInsurance(requestModel.ProductId);

            var insuranceResponseModel = new InsuranceResponseModel
            {
                ProductId = insurance.ProductId,
                InsuranceValue = insurance.InsuranceValue
            };

            return Ok(insuranceResponseModel);
        }
        
        [HttpPost]
        [Route("api/insurance/order")]
        public async Task<IActionResult> CalculateInsurance([FromBody] OrderInsuranceRequestModel requestModel)
        {
            var shoppingCartInsuranceResponseModel = new OrderInsuranceResponseModel();
            var productTypes = new List<int>();
            
            foreach (var productId in requestModel.ProductIds)
            {
                var insurance = await _insuranceDomainService.GetInsurance(productId);
                productTypes.Add(insurance.ProductTypeId);
                
                var insuranceResponseModel = new InsuranceResponseModel
                {
                    ProductId = insurance.ProductId,
                    InsuranceValue = insurance.InsuranceValue
                };
                
                shoppingCartInsuranceResponseModel.InsuranceResponseModels.Add(insuranceResponseModel);
            }

            var uniqueProductTypes = productTypes.Distinct();
            var orderSurcharge = await _insuranceDomainService.GetOrderSurcharge(uniqueProductTypes);
            shoppingCartInsuranceResponseModel.OrderSurcharge = orderSurcharge;
            
            return Ok(shoppingCartInsuranceResponseModel);
        }
    }
}