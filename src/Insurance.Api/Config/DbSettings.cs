namespace Insurance.Api.Config
{
    public interface IDbSettings
    {
        string InsuranceCollectionName { get; set; }
        string SurchargeCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DBName { get; set; }
    }
    public class DbSettings : IDbSettings
    {
        public string InsuranceCollectionName { get; set; }
        public string SurchargeCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DBName { get; set; }
    }
}