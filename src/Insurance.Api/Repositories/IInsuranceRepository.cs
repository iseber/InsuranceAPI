using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Api.Repositories.Models;
using MongoDB.Driver;

namespace Insurance.Api.Repositories
{
    public interface IInsuranceRepository
    {
        IMongoDatabase Database { get; }
        Task<float> GetInsuranceByProductPrice(float productPrice);
        Task<float> GetSurchargeByProductTypeId(int productTypeId);
        Task<float> GetOrderSurchargeByProductTypeIds(IEnumerable<int> productTypes);
        Task<Surcharge> GetSurchargeModelByProductTypeId(int productTypeId);
        Task Create(Surcharge surcharge);
        Task Update(int productTypeId, Surcharge surcharge);
        Task Upsert(Surcharge surcharge);
    }
}