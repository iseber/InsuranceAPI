using System.Threading.Tasks;
using Insurance.Api.Config;
using Insurance.Api.Repositories.Models;
using MongoDB.Driver;

namespace Insurance.Api.Repositories
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly IMongoCollection<Repositories.Models.Insurance> _insurances;
        private readonly IMongoCollection<Repositories.Models.Surcharge> _surcharges;

        private readonly IDbSettings _dbSettings;
        
        public InsuranceRepository(IDbSettings dbSettings)
        {
            _dbSettings = dbSettings;
            
            var client = new MongoClient(_dbSettings.ConnectionString);
            var database = client.GetDatabase(_dbSettings.DBName);

            _insurances = database.GetCollection<Repositories.Models.Insurance>(_dbSettings.InsuranceCollectionName);
            _surcharges = database.GetCollection<Repositories.Models.Surcharge>(_dbSettings.SurchargeCollectionName);
        }

        public async Task<float> GetInsuranceByProductPrice(float productPrice)
        {
            var insurance = await _insurances.FindAsync(x=> productPrice > x.LowerLimit && productPrice < x.UpperLimit);

            var insuranceValue = insurance.FirstOrDefault();
            
            return insuranceValue?.InsuranceCost ?? 0;
        }

        public async Task<float> GetSurchargeByProductTypeId(int productTypeId)
        {
            var surcharge = await _surcharges.FindAsync(x => x.ProductTypeId == productTypeId);

            var surchargeValue = surcharge.FirstOrDefault();

            return surchargeValue?.SurchargeCost ?? 0;
        }
        
        public async Task<Surcharge> GetSurchargeModelByProductTypeId(int productTypeId)
        {
            var surcharge = await _surcharges.FindAsync(x => x.ProductTypeId == productTypeId);

            return surcharge.FirstOrDefault();
        }

        public async Task Create(Surcharge surcharge)
        {
            await _surcharges.InsertOneAsync(surcharge);
        }

        public async Task Update(int productTypeId, Surcharge surcharge)
        {
            await _surcharges.ReplaceOneAsync(s => s.ProductTypeId == productTypeId, surcharge);
        }

        public async Task Upsert(Surcharge surcharge)
        {
            var existingSurcharge = await GetSurchargeModelByProductTypeId(surcharge.ProductTypeId);
            if (existingSurcharge == null)
            {
                await Create(existingSurcharge);
            }
            else
            {
                surcharge.ProductTypeId = existingSurcharge.ProductTypeId;
                Update(existingSurcharge.ProductTypeId, surcharge);
            }
        }
    }
}