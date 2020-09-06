using System.Threading.Tasks;
using Insurance.Api.Models;

namespace Insurance.Api.Services
{
    public interface IBusinessRules
    {
        Task<ProductResponseModel> GetProductTypeAndSalesPrice(int productId);
    }
}