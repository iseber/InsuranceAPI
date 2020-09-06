using System;
using Insurance.Api.Repositories;
using Insurance.Api.Repositories.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Insurance.Api.Config
{
    public class SeedDatabase
    {
        private static readonly ILogger logger = Log.ForContext<SeedDatabase>();

        public static void Initialize(IServiceProvider serviceProvider)
        {
            logger.Information("Database seed started...");
            
            var dbSettngs = serviceProvider.GetRequiredService<IDbSettings>();
            var context = serviceProvider.GetRequiredService<IInsuranceRepository>();
            
            context.Database.DropCollection("Insurances");
            context.Database.DropCollection("Surcharges");
            context.Database.DropCollection("OrderSurcharges");

            var insuranceCollection = context.Database.GetCollection<Repositories.Models.Insurance>(dbSettngs.InsuranceCollectionName);
            var surchargeCollection = context.Database.GetCollection<Repositories.Models.Surcharge>(dbSettngs.SurchargeCollectionName);
            var orderSurchargeCollection = context.Database.GetCollection<Repositories.Models.OrderSurcharge>(dbSettngs.OrderSurchargeCollectionName);
            
            insuranceCollection.InsertOne(new Repositories.Models.Insurance{ LowerLimit = 0, UpperLimit = 499, InsuranceCost = 0});
            insuranceCollection.InsertOne(new Repositories.Models.Insurance{ LowerLimit = 500, UpperLimit = 1999, InsuranceCost = 1000});
            insuranceCollection.InsertOne(new Repositories.Models.Insurance{ LowerLimit = 2000, UpperLimit = 9999, InsuranceCost = 2000});
            
            surchargeCollection.InsertOne(new Surcharge{ProductTypeId = 21, SurchargeCost = 500});
            surchargeCollection.InsertOne(new Surcharge{ProductTypeId = 32, SurchargeCost = 500});
            
            orderSurchargeCollection.InsertOne(new OrderSurcharge{ProductTypeId = 841, OrderSurchargeCost = 500});
            
            logger.Information("Database seed end...");
        }
    }
}