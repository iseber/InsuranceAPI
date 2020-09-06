using System.Threading.Tasks;
using Insurance.Api.Models;
using Insurance.Api.Repositories;
using Insurance.Api.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    public class AdmininstrationController : Controller
    {
        private readonly IInsuranceRepository _insuranceRepository;
        
        public AdmininstrationController(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
        }
        
        [HttpPost]
        [Route("api/insurance/surcharges")]
        public async Task<IActionResult> InsertSurcharges([FromBody] UploadSurchargesRequestModel requestModel)
        {
            foreach (var productSurcharge in requestModel.ProductSurcharges)
            {
                var surcharge = new Surcharge
                { 
                    ProductTypeId = productSurcharge.ProductTypeId,
                    SurchargeCost = productSurcharge.Surcharge
                };

                await _insuranceRepository.Upsert(surcharge);
            }
            
            return Ok();
        }
    }
}