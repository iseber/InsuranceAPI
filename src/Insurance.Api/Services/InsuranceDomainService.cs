using System.Threading.Tasks;
using Insurance.Api.Repositories;

namespace Insurance.Api.Services
{
    public class InsuranceDomainService : IInsuranceDomainService
    {
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IBusinessRules _businessRules;
        
        public InsuranceDomainService(IInsuranceRepository insuranceRepository, IBusinessRules businessRules)
        {
            _insuranceRepository = insuranceRepository;
            _businessRules = businessRules;
        }

        public async Task<Domain.Insurance> GetInsurance(int productId)
        {
            var productResponseModel = await _businessRules.GetProductTypeAndSalesPrice(productId);

            var insurance = Domain.Insurance.New(productId, productResponseModel.SalesPrice,
                productResponseModel.ProductTypeHasInsurance, productResponseModel.ProductTypeName);

            var insuranceValue = await _insuranceRepository.GetInsuranceByProductPrice(productResponseModel.SalesPrice);
            var surchargeValue =
                await _insuranceRepository.GetSurchargeByProductTypeId(productResponseModel.ProductTypeId);

            insurance.CalculateInsuranceValue(insuranceValue);
            insurance.CalculateSurcharge(surchargeValue);

            return insurance;
        }
    }
}