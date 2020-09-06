using System.Threading.Tasks;

namespace Insurance.Api.Repositories
{
    public interface IInsuranceRepository
    {
        Task<float> GetInsuranceByProductPrice(float productPrice);
        Task<float> GetSurchargeByProductTypeId(int productTypeId);
    }
}