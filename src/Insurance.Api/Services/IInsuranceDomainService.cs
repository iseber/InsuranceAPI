using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Api.Services
{
    public interface IInsuranceDomainService
    {
        Task<Domain.Insurance> GetInsurance(int productId);
        Task<float> GetOrderSurcharge(IEnumerable<int> productTypes);
    }
}