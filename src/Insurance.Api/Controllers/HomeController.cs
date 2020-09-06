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
            
            foreach (var product in requestModel.Products)
            {
                var insurance = await _insuranceDomainService.GetInsurance(product.ProductId);

                var insuranceResponseModel = new InsuranceResponseModel
                {
                    ProductId = insurance.ProductId,
                    InsuranceValue = insurance.InsuranceValue
                };
                
                shoppingCartInsuranceResponseModel.InsuranceResponseModels.Add(insuranceResponseModel);
            }

            
            return Ok(shoppingCartInsuranceResponseModel);
        }
    }
}