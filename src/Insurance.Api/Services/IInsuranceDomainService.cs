using System.Threading.Tasks;

namespace Insurance.Api.Services
{
    public interface IInsuranceDomainService
    {
        Task<Domain.Insurance> GetInsurance(int productId);
    }
}