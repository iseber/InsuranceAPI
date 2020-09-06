using System.Threading.Tasks;
using Insurance.Api.Repositories.Models;

namespace Insurance.Api.Repositories
{
    public interface IInsuranceRepository
    {
        Task<float> GetInsuranceByProductPrice(float productPrice);
        Task<float> GetSurchargeByProductTypeId(int productTypeId);
        Task<Surcharge> GetSurchargeModelByProductTypeId(int productTypeId);
        Task Create(Surcharge surcharge);
        Task Update(int productTypeId, Surcharge surcharge);
        Task Upsert(Surcharge surcharge);
    }
}