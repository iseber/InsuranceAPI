using System.Collections.Generic;
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
    public class OrderInsuranceTests
    {
        private readonly ControllerTestFixture _fixture;

        public OrderInsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public async Task CalculateInsurance_GivenOrderWithMultipleProducts_ShouldAddThousandAndFiveHundredEurosToOrderInsuranceCost()
        {
            const float expectedInsuranceValue = 1500;
            
            var insuranceRepositoryMock = new Mock<IInsuranceRepository>();
            insuranceRepositoryMock.Setup(x => x.GetInsuranceByProductPrice(750)).ReturnsAsync(1000);
            insuranceRepositoryMock.Setup(x => x.GetInsuranceByProductPrice(450)).ReturnsAsync(0);
            insuranceRepositoryMock.Setup(x => x.GetSurchargeByProductTypeId(2)).ReturnsAsync(500);
            insuranceRepositoryMock.Setup(x => x.GetOrderSurchargeByProductTypeIds(new[] {1, 2})).ReturnsAsync(0);
            var insuranceRepository = insuranceRepositoryMock.Object;
            var businessRules = new BusinessRules(MockHelper.AppSettings());

            var insuranceDomainService = new InsuranceDomainService(insuranceRepository, businessRules);
            var sut = new HomeController(insuranceDomainService);
            
            var insuranceRequestModel = new OrderInsuranceRequestModel {ProductIds = new List<int> {1, 2}};
            var result = await sut.CalculateInsurance(insuranceRequestModel) as ObjectResult;
        
            var model = result.Value as OrderInsuranceResponseModel;
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: model.SumOfOrderInsurance
            );
            
            insuranceRepositoryMock.VerifyAll();
        }
    }
}