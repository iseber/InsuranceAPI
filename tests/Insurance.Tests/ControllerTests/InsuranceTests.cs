using System.Threading.Tasks;
using Insurance.Api.Controllers;
using Insurance.Api.Models;
using Insurance.Api.Repositories;
using Insurance.Api.Services;
using Insurance.Tests.Configuration;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Insurance.Tests.ControllerTests
{
    [Collection("Controller collection")]

    public class InsuranceTests
    {
        private readonly ControllerTestFixture _fixture;

        public InsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAdd1000EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1000;
            
            var insuranceRepositoryMock = new Mock<IInsuranceRepository>();
            insuranceRepositoryMock.Setup(x => x.GetInsuranceByProductPrice(It.IsAny<float>())).ReturnsAsync(1000);
            var insuranceRepository = insuranceRepositoryMock.Object;
            var businessRules = new BusinessRules(MockHelper.AppSettings());

            var insuranceDomainService = new InsuranceDomainService(insuranceRepository, businessRules);
            var sut = new HomeController(insuranceDomainService);
            
            var insuranceRequestModel = new InsuranceRequestModel {ProductId = 1};
            var result = await sut.CalculateInsurance(insuranceRequestModel) as ObjectResult;
        
            var model = result.Value as InsuranceResponseModel;
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: model.InsuranceValue
            );
        }
        
        [Fact]
        public async Task CalculateInsurance_GivenSalesPriceBelow500AndProductTypeLaptop_ShouldAddF5000EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 500;
            
            var insuranceRepositoryMock = new Mock<IInsuranceRepository>();
            insuranceRepositoryMock.Setup(x => x.GetInsuranceByProductPrice(It.IsAny<float>())).ReturnsAsync(0);
            insuranceRepositoryMock.Setup(x => x.GetSurchargeByProductTypeId(It.IsAny<int>())).ReturnsAsync(500);
            var insuranceRepository = insuranceRepositoryMock.Object;
            var businessRules = new BusinessRules(MockHelper.AppSettings());
            
            var insuranceDomainService = new InsuranceDomainService(insuranceRepository, businessRules);
            var sut = new HomeController(insuranceDomainService);

            var insuranceRequestModel = new InsuranceRequestModel {ProductId = 2};
            var result = await sut.CalculateInsurance(insuranceRequestModel) as ObjectResult;
        
            var model = result.Value as InsuranceResponseModel;
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: model.InsuranceValue
            );
        }

        [Fact]
        public async Task CalculateInsurance_GivenSalesPriceAbove2000Euros_ShouldAdd2000EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 2000;
            
            var insuranceRepositoryMock = new Mock<IInsuranceRepository>();
            insuranceRepositoryMock.Setup(x => x.GetInsuranceByProductPrice(It.IsAny<float>())).ReturnsAsync(2000);
            var insuranceRepository = insuranceRepositoryMock.Object;
            var businessRules = new BusinessRules(MockHelper.AppSettings());

            var insuranceDomainService = new InsuranceDomainService(insuranceRepository, businessRules);
            var sut = new HomeController(insuranceDomainService);
            
            var insuranceRequestModel = new InsuranceRequestModel {ProductId = 4};
            var result = await sut.CalculateInsurance(insuranceRequestModel) as ObjectResult;
        
            var model = result.Value as InsuranceResponseModel;
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: model.InsuranceValue
            );
        }
        
        [Fact]
        public async Task CalculateInsurance_GivenSalesPriceBelow500AndProductTypeSmartphone_ShouldAddF5000EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1500;
            
            var insuranceRepositoryMock = new Mock<IInsuranceRepository>();
            insuranceRepositoryMock.Setup(x => x.GetInsuranceByProductPrice(It.IsAny<float>())).ReturnsAsync(1000);
            insuranceRepositoryMock.Setup(x => x.GetSurchargeByProductTypeId(It.IsAny<int>())).ReturnsAsync(500);
            
            var insuranceRepository = insuranceRepositoryMock.Object;
            var businessRules = new BusinessRules(MockHelper.AppSettings());
            
            var insuranceDomainService = new InsuranceDomainService(insuranceRepository, businessRules);
            var sut = new HomeController(insuranceDomainService);

            var insuranceRequestModel = new InsuranceRequestModel {ProductId = 5};
            var result = await sut.CalculateInsurance(insuranceRequestModel) as ObjectResult;
        
            var model = result.Value as InsuranceResponseModel;
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: model.InsuranceValue
            );
        }
    }
}